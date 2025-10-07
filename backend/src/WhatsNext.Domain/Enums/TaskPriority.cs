// <copyright file="TaskPriority.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace WhatsNext.Domain.Enums;

/// <summary>
/// Represents the priority level of a task.
/// </summary>
public enum TaskPriority
{
    /// <summary>
    /// Low priority task.
    /// </summary>
    Low = 0,

    /// <summary>
    /// Medium priority task.
    /// </summary>
    Medium = 1,

    /// <summary>
    /// High priority task.
    /// </summary>
    High = 2,
}
