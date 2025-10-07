// <copyright file="TimerSession.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using WhatsNext.Domain.Common;
using WhatsNext.Domain.Enums;

namespace WhatsNext.Domain.Entities;

/// <summary>
/// Represents a Pomodoro timer session.
/// </summary>
public class TimerSession : BaseEntity
{
    /// <summary>
    /// Gets or sets the user identifier who owns this session.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user who owns this session.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of session (Work, ShortBreak, LongBreak).
    /// </summary>
    public SessionType SessionType { get; set; }

    /// <summary>
    /// Gets or sets the duration of the session in minutes.
    /// </summary>
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the start time of the session.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the session.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the session was completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this session.
    /// </summary>
    public string? Notes { get; set; }
}
