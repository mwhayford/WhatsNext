// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WhatsNext.Domain.Enums;

/// <summary>
/// Represents the frequency at which a habit should be performed.
/// </summary>
public enum HabitFrequency
{
    /// <summary>
    /// The habit should be performed daily.
    /// </summary>
    Daily = 0,

    /// <summary>
    /// The habit should be performed weekly.
    /// </summary>
    Weekly = 1,

    /// <summary>
    /// The habit should be performed monthly.
    /// </summary>
    Monthly = 2,

    /// <summary>
    /// The habit should be performed on specific days.
    /// </summary>
    Custom = 3,
}

