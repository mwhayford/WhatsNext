// <copyright file="HabitConfiguration.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity configuration for Habit entity.
/// </summary>
public class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.ToTable("Habits");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.Description)
            .HasMaxLength(1000);

        builder.Property(h => h.Color)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("#3B82F6");

        builder.Property(h => h.Icon)
            .HasMaxLength(50);

        builder.Property(h => h.Frequency)
            .IsRequired();

        builder.Property(h => h.TargetCount)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(h => h.CurrentStreak)
            .HasDefaultValue(0);

        builder.Property(h => h.LongestStreak)
            .HasDefaultValue(0);

        builder.Property(h => h.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Relationships
        builder.HasOne(h => h.User)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.Completions)
            .WithOne(hc => hc.Habit)
            .HasForeignKey(hc => hc.HabitId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(h => h.UserId);
        builder.HasIndex(h => h.StartDate);
        builder.HasIndex(h => h.IsActive);

        // Query filter for soft delete
        builder.HasQueryFilter(h => !h.IsDeleted);
    }
}
