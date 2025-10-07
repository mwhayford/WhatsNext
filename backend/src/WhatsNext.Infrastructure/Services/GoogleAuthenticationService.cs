// <copyright file="GoogleAuthenticationService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text.Json;
using Microsoft.Extensions.Logging;
using WhatsNext.Application.Common.Interfaces;

namespace WhatsNext.Infrastructure.Services;

/// <summary>
/// Google OAuth authentication service implementation.
/// </summary>
public class GoogleAuthenticationService : IExternalAuthenticationService
{
    private readonly HttpClient httpClient;
    private readonly ILogger<GoogleAuthenticationService> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GoogleAuthenticationService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client for making requests to Google APIs.</param>
    /// <param name="logger">Logger instance.</param>
    public GoogleAuthenticationService(HttpClient httpClient, ILogger<GoogleAuthenticationService> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ExternalAuthenticationResult?> AuthenticateAsync(
        string provider, 
        string accessToken, 
        CancellationToken cancellationToken = default)
    {
        if (provider != "Google")
        {
            this.logger.LogWarning("Unsupported provider: {Provider}", provider);
            return null;
        }

        try
        {
            // Validate the access token with Google
            var userInfo = await this.GetGoogleUserInfoAsync(accessToken, cancellationToken);
            
            if (userInfo == null)
            {
                this.logger.LogWarning("Failed to retrieve user info from Google");
                return null;
            }

            return new ExternalAuthenticationResult
            {
                ExternalUserId = userInfo.Id,
                Email = userInfo.Email,
                DisplayName = userInfo.Name,
                FirstName = userInfo.GivenName,
                LastName = userInfo.FamilyName,
                ProfilePictureUrl = userInfo.Picture,
                Provider = "Google",
                AdditionalClaims = new Dictionary<string, string>
                {
                    ["locale"] = userInfo.Locale ?? string.Empty,
                    ["verified_email"] = userInfo.VerifiedEmail.ToString(),
                }
            };
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error authenticating with Google for provider {Provider}", provider);
            return null;
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> GetSupportedProviders()
    {
        return new[] { "Google" };
    }

    /// <summary>
    /// Retrieves user information from Google using the access token.
    /// </summary>
    /// <param name="accessToken">Google access token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Google user information or null if failed.</returns>
    private async Task<GoogleUserInfo?> GetGoogleUserInfoAsync(string accessToken, CancellationToken cancellationToken)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await this.httpClient.SendAsync(request, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                this.logger.LogWarning("Google API returned status code: {StatusCode}", response.StatusCode);
                return null;
            }

            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var userInfo = JsonSerializer.Deserialize<GoogleUserInfo>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            this.logger.LogInformation("Successfully retrieved user info from Google for user: {Email}", userInfo?.Email);
            return userInfo;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error retrieving user info from Google");
            return null;
        }
    }
}

/// <summary>
/// Google user information model.
/// </summary>
public class GoogleUserInfo
{
    /// <summary>
    /// Gets or sets the Google user ID.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the verified email status.
    /// </summary>
    public bool VerifiedEmail { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the given name (first name).
    /// </summary>
    public string GivenName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the family name (last name).
    /// </summary>
    public string FamilyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the profile picture URL.
    /// </summary>
    public string Picture { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the locale.
    /// </summary>
    public string? Locale { get; set; }
}
