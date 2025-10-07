// <copyright file="AuthenticationController.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using MediatR;
using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    public AuthenticationController(IMediator mediator)
    {
        this.mediator = mediator;
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
}

