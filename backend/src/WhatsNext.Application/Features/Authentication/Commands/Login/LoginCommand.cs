// <copyright file="LoginCommand.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using MediatR;
using WhatsNext.Application.Common.Models;

namespace WhatsNext.Application.Features.Authentication.Commands.Login;

/// <summary>
/// Command to login a user.
/// </summary>
public class LoginCommand : IRequest<AuthenticationResult>
{
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}

