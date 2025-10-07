// <copyright file="AuthenticationController.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhatsNext.Application.Common.Interfaces;
using WhatsNext.Application.Features.Authentication.Commands.Login;
using WhatsNext.Application.Features.Authentication.Commands.Register;

namespace WhatsNext.API.Controllers;

/// <summary>
/// Controller for authentication operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IAuthenticationService authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    /// <param name="authService">The authentication service.</param>
    public AuthenticationController(IMediator mediator, IAuthenticationService authService)
    {
        this.mediator = mediator;
        this.authService = authService;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="command">The registration command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authentication result with token.</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await this.mediator.Send(command, cancellationToken);
            return this.Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return this.BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="command">The login command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authentication result with token.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await this.mediator.Send(command, cancellationToken);
            return this.Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return this.Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Success message.</returns>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        try
        {
            // Get token ID from JWT claims
            var tokenId = this.User.FindFirst("token_id")?.Value;
            if (string.IsNullOrEmpty(tokenId))
            {
                return this.BadRequest(new { message = "Invalid token." });
            }

            var success = await this.authService.LogoutAsync(tokenId, cancellationToken);
            if (success)
            {
                return this.Ok(new { message = "Logged out successfully." });
            }

            return this.BadRequest(new { message = "Logout failed." });
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, new { message = "An error occurred during logout." });
        }
    }

    /// <summary>
    /// Logs out all sessions for the current user.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Success message.</returns>
    [HttpPost("logout-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAll(CancellationToken cancellationToken)
    {
        try
        {
            // Get user ID from JWT claims
            var userIdClaim = this.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return this.BadRequest(new { message = "Invalid user." });
            }

            var success = await this.authService.LogoutAllAsync(userId, cancellationToken);
            if (success)
            {
                return this.Ok(new { message = "All sessions logged out successfully." });
            }

            return this.BadRequest(new { message = "Logout all failed." });
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, new { message = "An error occurred during logout all." });
        }
    }

    /// <summary>
    /// Authenticates a user with Google OAuth.
    /// </summary>
    /// <param name="request">The Google OAuth request containing the access token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authentication result with JWT token.</returns>
    [HttpPost("google")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GoogleAuth([FromBody] GoogleAuthRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken))
            {
                return this.BadRequest(new { message = "Access token is required." });
            }

            var result = await this.authService.AuthenticateWithExternalProviderAsync("Google", request.AccessToken, cancellationToken);
            
            if (result == null)
            {
                return this.Unauthorized(new { message = "Google authentication failed." });
            }

            return this.Ok(result);
        }
        catch (Exception ex)
        {
            return this.StatusCode(500, new { message = "An error occurred during Google authentication." });
        }
    }

    /// <summary>
    /// Gets the supported OAuth providers.
    /// </summary>
    /// <returns>List of supported providers.</returns>
    [HttpGet("providers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetProviders()
    {
        return this.Ok(new { providers = new[] { "Google" } });
    }
}

/// <summary>
/// Google OAuth authentication request.
/// </summary>
public class GoogleAuthRequest
{
    /// <summary>
    /// Gets or sets the Google access token.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;
}
