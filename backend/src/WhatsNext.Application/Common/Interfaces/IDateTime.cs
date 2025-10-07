// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WhatsNext.Application.Common.Interfaces;

/// <summary>
/// Interface for date and time services.
/// </summary>
public interface IDateTime
{
    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    DateTime UtcNow { get; }

    /// <summary>
    /// Gets the current local date and time.
    /// </summary>
    DateTime Now { get; }
}

