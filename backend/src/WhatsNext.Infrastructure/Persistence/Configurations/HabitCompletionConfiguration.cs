// <copyright file="HabitCompletionConfiguration.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity configuration for HabitCompletion entity.
/// </summary>
public class HabitCompletionConfiguration : IEntityTypeConfiguration<HabitCompletion>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<HabitCompletion> builder)
    {
        builder.ToTable("HabitCompletions");

        builder.HasKey(hc => hc.Id);

        builder.Property(hc => hc.CompletedDate)
            .IsRequired();

        builder.Property(hc => hc.Notes)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(hc => hc.Habit)
            .WithMany(h => h.Completions)
            .HasForeignKey(hc => hc.HabitId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(hc => hc.HabitId);
        builder.HasIndex(hc => hc.CompletedDate);
        builder.HasIndex(hc => new { hc.HabitId, hc.CompletedDate });

        // Query filter for soft delete
        builder.HasQueryFilter(hc => !hc.IsDeleted);
    }
}
