// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WhatsNext.Application.Common.Interfaces;

/// <summary>
/// Interface for getting current user information.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user identifier.
    /// </summary>
    int? UserId { get; }

    /// <summary>
    /// Gets the current user's email.
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Gets a value indicating whether the user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
}

