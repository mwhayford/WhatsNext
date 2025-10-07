// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WhatsNext.Domain.Enums;

/// <summary>
/// Represents the status of a task.
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// Task is not started.
    /// </summary>
    Todo = 0,

    /// <summary>
    /// Task is in progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Task is completed.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Task is cancelled.
    /// </summary>
    Cancelled = 3,
}

