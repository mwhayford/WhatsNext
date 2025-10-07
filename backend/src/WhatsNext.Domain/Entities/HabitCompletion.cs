// <copyright file="HabitCompletion.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using WhatsNext.Domain.Common;

namespace WhatsNext.Domain.Entities;

/// <summary>
/// Represents a completion record for a habit.
/// </summary>
public class HabitCompletion : BaseEntity
{
    /// <summary>
    /// Gets or sets the habit identifier.
    /// </summary>
    public int HabitId { get; set; }

    /// <summary>
    /// Gets or sets the habit this completion belongs to.
    /// </summary>
    public Habit Habit { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date when the habit was completed.
    /// </summary>
    public DateTime CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this completion.
    /// </summary>
    public string? Notes { get; set; }
}
