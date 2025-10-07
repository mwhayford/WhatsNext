// <copyright file="IAuthenticationService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using WhatsNext.Application.Common.Models;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Application.Common.Interfaces;

/// <summary>
/// Interface for authentication services.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user to generate a token for.</param>
    /// <returns>An authentication result containing the token.</returns>
    AuthenticationResult GenerateToken(User user);

    /// <summary>
    /// Validates a refresh token and generates a new access token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An authentication result with a new token, or null if invalid.</returns>
    Task<AuthenticationResult?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    /// <summary>
    /// Hashes a password using BCrypt.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The hashed password.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a password against a hash.
    /// </summary>
    /// <param name="password">The password to verify.</param>
    /// <param name="hash">The hash to verify against.</param>
    /// <returns>True if the password matches the hash.</returns>
    bool VerifyPassword(string password, string hash);
}

