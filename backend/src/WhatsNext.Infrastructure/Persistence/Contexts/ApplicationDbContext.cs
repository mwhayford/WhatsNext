// <copyright file="ApplicationDbContext.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Reflection;

using Microsoft.EntityFrameworkCore;
using WhatsNext.Application.Common.Interfaces;
using WhatsNext.Domain.Common;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Persistence.Contexts;

/// <summary>
/// Application database context for Entity Framework Core.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDateTime dateTime;
    private readonly ICurrentUserService? currentUserService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    /// <param name="dateTime">The date time service.</param>
    /// <param name="currentUserService">The current user service (optional for migrations).</param>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDateTime dateTime,
        ICurrentUserService? currentUserService = null)
        : base(options)
    {
        this.dateTime = dateTime;
        this.currentUserService = currentUserService;
    }

    /// <inheritdoc/>
    public DbSet<User> Users { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Habit> Habits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<HabitCompletion> HabitCompletions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TodoTask> Tasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TimerSession> TimerSessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Quote> Quotes { get; set; } = null!;

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = this.dateTime.UtcNow;
                    if (entry.Entity is AuditableEntity auditableEntity)
                    {
                        auditableEntity.CreatedBy = this.currentUserService?.UserId;
                    }

                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = this.dateTime.UtcNow;
                    if (entry.Entity is AuditableEntity modifiedAuditableEntity)
                    {
                        modifiedAuditableEntity.UpdatedBy = this.currentUserService?.UserId;
                    }

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
