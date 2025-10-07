// <copyright file="LoginCommandHandler.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using MediatR;
using Microsoft.EntityFrameworkCore;
using WhatsNext.Application.Common.Interfaces;
using WhatsNext.Application.Common.Models;

namespace WhatsNext.Application.Features.Authentication.Commands.Login;

/// <summary>
/// Handler for the LoginCommand.
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResult>
{
    private readonly IApplicationDbContext context;
    private readonly IAuthenticationService authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="authService">The authentication service.</param>
    public LoginCommandHandler(
        IApplicationDbContext context,
        IAuthenticationService authService)
    {
        this.context = context;
        this.authService = authService;
    }

    /// <inheritdoc/>
    public async Task<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await this.context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Verify password
        if (!this.authService.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Generate token
        return this.authService.GenerateToken(user);
    }
}

