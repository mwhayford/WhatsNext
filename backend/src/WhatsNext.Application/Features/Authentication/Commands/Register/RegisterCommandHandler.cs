// <copyright file="RegisterCommandHandler.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using MediatR;
using Microsoft.EntityFrameworkCore;
using WhatsNext.Application.Common.Interfaces;
using WhatsNext.Application.Common.Models;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Application.Features.Authentication.Commands.Register;

/// <summary>
/// Handler for the RegisterCommand.
/// </summary>
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IApplicationDbContext context;
    private readonly IAuthenticationService authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="authService">The authentication service.</param>
    public RegisterCommandHandler(
        IApplicationDbContext context,
        IAuthenticationService authService)
    {
        this.context = context;
        this.authService = authService;
    }

    /// <inheritdoc/>
    public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check if username already exists
        var usernameExists = await this.context.Users
            .AnyAsync(u => u.Username == request.Username, cancellationToken);

        if (usernameExists)
        {
            throw new InvalidOperationException("Username is already taken.");
        }

        // Check if email already exists
        var emailExists = await this.context.Users
            .AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        // Create new user
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = this.authService.HashPassword(request.Password),
            Roles = new List<string> { "User" }, // Default role
        };

        this.context.Users.Add(user);
        await this.context.SaveChangesAsync(cancellationToken);

        // Generate token
        return this.authService.GenerateToken(user);
    }
}

