// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity configuration for Quote entity.
/// </summary>
public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.ToTable("Quotes");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(q => q.Author)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(q => q.Category)
            .HasMaxLength(100);

        builder.Property(q => q.Source)
            .HasMaxLength(300);

        // Relationships
        builder.HasMany(q => q.FavoritedByUsers)
            .WithMany(u => u.FavoriteQuotes)
            .UsingEntity(j => j.ToTable("UserFavoriteQuotes"));

        // Indexes
        builder.HasIndex(q => q.Author);
        builder.HasIndex(q => q.Category);

        // Query filter for soft delete
        builder.HasQueryFilter(q => !q.IsDeleted);
    }
}

