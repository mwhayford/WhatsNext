// <copyright file="AuthenticationService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WhatsNext.Application.Common.Interfaces;
using WhatsNext.Application.Common.Models;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Services;

/// <summary>
/// Service for handling authentication operations.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration configuration;
    private readonly IApplicationDbContext context;
    private readonly ISessionService sessionService;
    private readonly ILogger<AuthenticationService> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="context">The database context.</param>
    /// <param name="sessionService">The session service.</param>
    /// <param name="logger">The logger.</param>
    public AuthenticationService(
        IConfiguration configuration, 
        IApplicationDbContext context, 
        ISessionService sessionService,
        ILogger<AuthenticationService> logger)
    {
        this.configuration = configuration;
        this.context = context;
        this.sessionService = sessionService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task<AuthenticationResult> GenerateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var jwtSettings = this.configuration.GetSection("JwtSettings");
        var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret is not configured.");
        var issuer = jwtSettings["Issuer"] ?? "WhatsNext";
        var audience = jwtSettings["Audience"] ?? "WhatsNext";
        var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");
        var refreshTokenExpiryDays = int.Parse(jwtSettings["RefreshTokenExpiryDays"] ?? "7");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);
        var refreshExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);

        // Generate unique token ID for session management
        var tokenId = Guid.NewGuid().ToString();
        var refreshToken = GenerateSecureRefreshToken();

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, tokenId),
            new Claim("username", user.Username),
            new Claim("token_id", tokenId),
        };

        // Add roles as claims
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Store token in Redis for session management
        await this.sessionService.StoreTokenAsync(user.Id, tokenId, tokenString, expiresAt, cancellationToken);

        // Store session data
        var sessionData = new
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = user.Roles,
            LoginTime = DateTime.UtcNow,
        };

        await this.sessionService.StoreSessionAsync(user.Id, sessionData, refreshExpiresAt, cancellationToken);

        this.logger.LogInformation("Generated token for user {UserId} with token ID {TokenId}", user.Id, tokenId);

        return new AuthenticationResult
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            Token = tokenString,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            Roles = user.Roles.ToList(),
        };
    }

    /// <summary>
    /// Generates a secure refresh token.
    /// </summary>
    /// <returns>A secure refresh token.</returns>
    private static string GenerateSecureRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    /// <inheritdoc/>
    public async Task<AuthenticationResult?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        // In a production app, you would:
        // 1. Validate the refresh token against stored tokens in the database
        // 2. Check if it's expired
        // 3. Generate a new access token
        // 4. Optionally rotate the refresh token

        // For now, returning null (simplified implementation)
        await Task.CompletedTask;
        return null;
    }

    /// <inheritdoc/>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    /// <inheritdoc/>
    public async Task<bool> LogoutAsync(string tokenId, CancellationToken cancellationToken = default)
    {
        try
        {
            var blacklisted = await this.sessionService.BlacklistTokenAsync(tokenId, cancellationToken);
            if (blacklisted)
            {
                this.logger.LogInformation("User logged out successfully with token ID {TokenId}", tokenId);
            }
            return blacklisted;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error during logout for token ID {TokenId}", tokenId);
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> LogoutAllAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var revoked = await this.sessionService.RevokeAllUserTokensAsync(userId, cancellationToken);
            if (revoked)
            {
                this.logger.LogInformation("All sessions revoked for user {UserId}", userId);
            }
            return revoked;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error during logout all for user {UserId}", userId);
            return false;
        }
    }
}
