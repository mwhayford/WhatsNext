// <copyright file="IExternalAuthenticationService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using WhatsNext.Application.Common.Models;

namespace WhatsNext.Application.Common.Interfaces;

/// <summary>
/// Interface for external authentication providers (Google, Microsoft, GitHub, etc.).
/// </summary>
public interface IExternalAuthenticationService
{
    /// <summary>
    /// Authenticates a user with an external provider and returns user information.
    /// </summary>
    /// <param name="provider">The authentication provider (e.g., "Google", "Microsoft", "GitHub").</param>
    /// <param name="accessToken">The access token from the external provider.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>External authentication result with user information.</returns>
    Task<ExternalAuthenticationResult?> AuthenticateAsync(
        string provider, 
        string accessToken, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the supported authentication providers.
    /// </summary>
    /// <returns>List of supported provider names.</returns>
    IEnumerable<string> GetSupportedProviders();
}

/// <summary>
/// Result of external authentication.
/// </summary>
public class ExternalAuthenticationResult
{
    /// <summary>
    /// Gets or sets the external provider user ID.
    /// </summary>
    public string ExternalUserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the profile picture URL.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the provider name.
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional claims from the external provider.
    /// </summary>
    public Dictionary<string, string> AdditionalClaims { get; set; } = new();
}
