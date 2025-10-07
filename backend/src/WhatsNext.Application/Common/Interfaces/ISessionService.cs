// <copyright file="ISessionService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace WhatsNext.Application.Common.Interfaces;

/// <summary>
/// Interface for session management operations.
/// </summary>
public interface ISessionService
{
    /// <summary>
    /// Stores a JWT token in the session store.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="tokenId">The unique token identifier.</param>
    /// <param name="token">The JWT token.</param>
    /// <param name="expiry">Token expiration time.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the token was stored successfully.</returns>
    Task<bool> StoreTokenAsync(int userId, string tokenId, string token, DateTime expiry, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates if a token exists and is not blacklisted.
    /// </summary>
    /// <param name="tokenId">The unique token identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the token is valid.</returns>
    Task<bool> IsTokenValidAsync(string tokenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Blacklists a token (marks it as invalid).
    /// </summary>
    /// <param name="tokenId">The unique token identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the token was blacklisted successfully.</returns>
    Task<bool> BlacklistTokenAsync(string tokenId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active tokens for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of active token IDs.</returns>
    Task<IEnumerable<string>> GetUserTokensAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revokes all tokens for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if all tokens were revoked successfully.</returns>
    Task<bool> RevokeAllUserTokensAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cleans up expired tokens.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Number of tokens cleaned up.</returns>
    Task<int> CleanupExpiredTokensAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores user session data.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="sessionData">The session data to store.</param>
    /// <param name="expiry">Session expiration time.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the session was stored successfully.</returns>
    Task<bool> StoreSessionAsync(int userId, object sessionData, DateTime expiry, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user session data.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The session data if found.</returns>
    Task<T?> GetSessionAsync<T>(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes user session data.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the session was removed successfully.</returns>
    Task<bool> RemoveSessionAsync(int userId, CancellationToken cancellationToken = default);
}

