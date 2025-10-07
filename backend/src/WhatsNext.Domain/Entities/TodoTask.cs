// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WhatsNext.Domain.Common;
using WhatsNext.Domain.Enums;

using TaskStatus = WhatsNext.Domain.Enums.TaskStatus;

namespace WhatsNext.Domain.Entities;

/// <summary>
/// Represents a task in the task manager.
/// </summary>
public class TodoTask : AuditableEntity
{
    /// <summary>
    /// Gets or sets the user identifier who owns this task.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user who owns this task.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the title of the task.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the priority level of the task.
    /// </summary>
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public TaskStatus Status { get; set; } = TaskStatus.Todo;

    /// <summary>
    /// Gets or sets the due date of the task.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the task was completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is important.
    /// </summary>
    public bool IsImportant { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the task.
    /// </summary>
    public string? Tags { get; set; }
}

