// <copyright file="SessionService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.Extensions.Logging;
using WhatsNext.Application.Common.Interfaces;

namespace WhatsNext.Infrastructure.Services;

/// <summary>
/// Session management service implementation using Redis.
/// </summary>
public class SessionService : ISessionService
{
    private readonly IRedisService _redisService;
    private readonly ILogger<SessionService> _logger;

    // Redis key patterns
    private const string TokenKeyPrefix = "token:";
    private const string BlacklistKeyPrefix = "blacklist:";
    private const string UserTokensKeyPrefix = "user_tokens:";
    private const string SessionKeyPrefix = "session:";

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionService"/> class.
    /// </summary>
    /// <param name="redisService">Redis service.</param>
    /// <param name="logger">Logger.</param>
    public SessionService(IRedisService redisService, ILogger<SessionService> logger)
    {
        _redisService = redisService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> StoreTokenAsync(int userId, string tokenId, string token, DateTime expiry, CancellationToken cancellationToken = default)
    {
        try
        {
            var tokenKey = $"{TokenKeyPrefix}{tokenId}";
            var userTokensKey = $"{UserTokensKeyPrefix}{userId}";

            // Store the token
            var tokenExpiry = expiry - DateTime.UtcNow;
            var tokenStored = await _redisService.SetAsync(tokenKey, token, tokenExpiry, cancellationToken);

            if (tokenStored)
            {
                // Add token ID to user's token list
                var userTokens = await _redisService.GetAsync<List<string>>(userTokensKey, cancellationToken) ?? new List<string>();
                if (!userTokens.Contains(tokenId))
                {
                    userTokens.Add(tokenId);
                    await _redisService.SetAsync(userTokensKey, userTokens, TimeSpan.FromDays(30), cancellationToken);
                }

                _logger.LogDebug("Stored token {TokenId} for user {UserId}", tokenId, userId);
            }

            return tokenStored;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error storing token {TokenId} for user {UserId}", tokenId, userId);
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> IsTokenValidAsync(string tokenId, CancellationToken cancellationToken = default)
    {
        try
        {
            var tokenKey = $"{TokenKeyPrefix}{tokenId}";
            var blacklistKey = $"{BlacklistKeyPrefix}{tokenId}";

            // Check if token is blacklisted
            var isBlacklisted = await _redisService.ExistsAsync(blacklistKey, cancellationToken);
            if (isBlacklisted)
            {
                return false;
            }

            // Check if token exists
            var tokenExists = await _redisService.ExistsAsync(tokenKey, cancellationToken);
            return tokenExists;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating token {TokenId}", tokenId);
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> BlacklistTokenAsync(string tokenId, CancellationToken cancellationToken = default)
    {
        try
        {
            var blacklistKey = $"{BlacklistKeyPrefix}{tokenId}";
            var tokenKey = $"{TokenKeyPrefix}{tokenId}";

            // Add to blacklist with 7 days expiry (longer than token expiry)
            var blacklisted = await _redisService.SetAsync(blacklistKey, true, TimeSpan.FromDays(7), cancellationToken);

            // Remove the token from active tokens
            await _redisService.DeleteAsync(tokenKey, cancellationToken);

            _logger.LogDebug("Blacklisted token {TokenId}", tokenId);
            return blacklisted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error blacklisting token {TokenId}", tokenId);
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetUserTokensAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var userTokensKey = $"{UserTokensKeyPrefix}{userId}";
            var userTokens = await _redisService.GetAsync<List<string>>(userTokensKey, cancellationToken);
            return userTokens ?? Enumerable.Empty<string>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tokens for user {UserId}", userId);
            return Enumerable.Empty<string>();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> RevokeAllUserTokensAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var userTokens = await GetUserTokensAsync(userId, cancellationToken);
            var tasks = userTokens.Select(tokenId => BlacklistTokenAsync(tokenId, cancellationToken));
            await Task.WhenAll(tasks);

            // Clear user tokens list
            var userTokensKey = $"{UserTokensKeyPrefix}{userId}";
            await _redisService.DeleteAsync(userTokensKey, cancellationToken);

            _logger.LogInformation("Revoked all tokens for user {UserId}", userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error revoking all tokens for user {UserId}", userId);
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<int> CleanupExpiredTokensAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var tokenKeys = await _redisService.GetKeysAsync($"{TokenKeyPrefix}*", cancellationToken);
            var blacklistKeys = await _redisService.GetKeysAsync($"{BlacklistKeyPrefix}*", cancellationToken);

            var allKeys = tokenKeys.Concat(blacklistKeys).ToList();
            var cleanupCount = 0;

            foreach (var key in allKeys)
            {
                // Redis automatically removes expired keys, but we can check if they still exist
                var exists = await _redisService.ExistsAsync(key, cancellationToken);
                if (!exists)
                {
                    cleanupCount++;
                }
            }

            _logger.LogInformation("Cleaned up {Count} expired tokens", cleanupCount);
            return cleanupCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cleaning up expired tokens");
            return 0;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> StoreSessionAsync(int userId, object sessionData, DateTime expiry, CancellationToken cancellationToken = default)
    {
        try
        {
            var sessionKey = $"{SessionKeyPrefix}{userId}";
            var sessionExpiry = expiry - DateTime.UtcNow;
            var stored = await _redisService.SetAsync(sessionKey, sessionData, sessionExpiry, cancellationToken);

            if (stored)
            {
                _logger.LogDebug("Stored session for user {UserId}", userId);
            }

            return stored;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error storing session for user {UserId}", userId);
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<T?> GetSessionAsync<T>(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var sessionKey = $"{SessionKeyPrefix}{userId}";
            return await _redisService.GetAsync<T>(sessionKey, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting session for user {UserId}", userId);
            return default;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveSessionAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var sessionKey = $"{SessionKeyPrefix}{userId}";
            var removed = await _redisService.DeleteAsync(sessionKey, cancellationToken);

            if (removed)
            {
                _logger.LogDebug("Removed session for user {UserId}", userId);
            }

            return removed;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing session for user {UserId}", userId);
            return false;
        }
    }
}

