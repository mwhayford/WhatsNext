// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WhatsNext.Domain.Common;
using WhatsNext.Domain.Enums;

namespace WhatsNext.Domain.Entities;

/// <summary>
/// Represents a habit that a user is tracking.
/// </summary>
public class Habit : AuditableEntity
{
    /// <summary>
    /// Gets or sets the user identifier who owns this habit.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user who owns this habit.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the habit.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the habit.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the frequency of the habit (Daily, Weekly, etc.).
    /// </summary>
    public HabitFrequency Frequency { get; set; }

    /// <summary>
    /// Gets or sets the color associated with the habit (for UI purposes).
    /// </summary>
    public string Color { get; set; } = "#3B82F6";

    /// <summary>
    /// Gets or sets the icon or emoji associated with the habit.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the target count per frequency period.
    /// </summary>
    public int TargetCount { get; set; } = 1;

    /// <summary>
    /// Gets or sets the current streak count.
    /// </summary>
    public int CurrentStreak { get; set; }

    /// <summary>
    /// Gets or sets the longest streak count.
    /// </summary>
    public int LongestStreak { get; set; }

    /// <summary>
    /// Gets or sets the date when the habit was started.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the habit is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of habit completions.
    /// </summary>
    public ICollection<HabitCompletion> Completions { get; set; } = new List<HabitCompletion>();
}

