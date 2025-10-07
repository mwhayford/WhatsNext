// <copyright file="RegisterCommand.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using MediatR;
using WhatsNext.Application.Common.Models;

namespace WhatsNext.Application.Features.Authentication.Commands.Register;

/// <summary>
/// Command to register a new user.
/// </summary>
public class RegisterCommand : IRequest<AuthenticationResult>
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password confirmation.
    /// </summary>
    public string ConfirmPassword { get; set; } = string.Empty;
}

