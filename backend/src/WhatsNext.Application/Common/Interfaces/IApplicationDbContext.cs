// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Application.Common.Interfaces;

/// <summary>
/// Interface for the application database context.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets or sets the Users DbSet.
    /// </summary>
    DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the Habits DbSet.
    /// </summary>
    DbSet<Habit> Habits { get; set; }

    /// <summary>
    /// Gets or sets the HabitCompletions DbSet.
    /// </summary>
    DbSet<HabitCompletion> HabitCompletions { get; set; }

    /// <summary>
    /// Gets or sets the Tasks DbSet.
    /// </summary>
    DbSet<TodoTask> Tasks { get; set; }

    /// <summary>
    /// Gets or sets the TimerSessions DbSet.
    /// </summary>
    DbSet<TimerSession> TimerSessions { get; set; }

    /// <summary>
    /// Gets or sets the Quotes DbSet.
    /// </summary>
    DbSet<Quote> Quotes { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

