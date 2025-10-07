// <copyright file="TodoTaskConfiguration.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity configuration for TodoTask entity.
/// </summary>
public class TodoTaskConfiguration : IEntityTypeConfiguration<TodoTask>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<TodoTask> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(t => t.Description)
            .HasMaxLength(2000);

        builder.Property(t => t.Priority)
            .IsRequired();

        builder.Property(t => t.Status)
            .IsRequired();

        builder.Property(t => t.Tags)
            .HasMaxLength(500);

        // Relationships
        builder.HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(t => t.UserId);
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.Priority);
        builder.HasIndex(t => t.DueDate);
        builder.HasIndex(t => t.IsImportant);

        // Query filter for soft delete
        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}
