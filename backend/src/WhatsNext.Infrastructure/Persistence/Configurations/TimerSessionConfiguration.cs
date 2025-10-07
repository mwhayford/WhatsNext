// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity configuration for TimerSession entity.
/// </summary>
public class TimerSessionConfiguration : IEntityTypeConfiguration<TimerSession>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TimerSession> builder)
    {
        builder.ToTable("TimerSessions");

        builder.HasKey(ts => ts.Id);

        builder.Property(ts => ts.SessionType)
            .IsRequired();

        builder.Property(ts => ts.DurationMinutes)
            .IsRequired();

        builder.Property(ts => ts.StartTime)
            .IsRequired();

        builder.Property(ts => ts.Notes)
            .HasMaxLength(1000);

        // Relationships
        builder.HasOne(ts => ts.User)
            .WithMany(u => u.TimerSessions)
            .HasForeignKey(ts => ts.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(ts => ts.UserId);
        builder.HasIndex(ts => ts.StartTime);
        builder.HasIndex(ts => ts.SessionType);
        builder.HasIndex(ts => ts.IsCompleted);

        // Query filter for soft delete
        builder.HasQueryFilter(ts => !ts.IsDeleted);
    }
}

