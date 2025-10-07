// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WhatsNext.Domain.Enums;

/// <summary>
/// Represents the type of Pomodoro timer session.
/// </summary>
public enum SessionType
{
    /// <summary>
    /// A work session (typically 25 minutes).
    /// </summary>
    Work = 0,

    /// <summary>
    /// A short break (typically 5 minutes).
    /// </summary>
    ShortBreak = 1,

    /// <summary>
    /// A long break (typically 15-30 minutes).
    /// </summary>
    LongBreak = 2,
}

