// <copyright file="BaseEntity.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace WhatsNext.Domain.Common;

/// <summary>
/// Base class for all entities in the domain.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted (soft delete).
    /// </summary>
    public bool IsDeleted { get; set; }
}
