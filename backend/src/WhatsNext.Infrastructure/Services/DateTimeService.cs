// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WhatsNext.Application.Common.Interfaces;

namespace WhatsNext.Infrastructure.Services;

/// <summary>
/// Service for getting current date and time.
/// </summary>
public class DateTimeService : IDateTime
{
    /// <inheritdoc/>
    public DateTime UtcNow => DateTime.UtcNow;

    /// <inheritdoc/>
    public DateTime Now => DateTime.Now;
}

