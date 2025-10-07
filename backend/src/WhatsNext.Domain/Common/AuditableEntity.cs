// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WhatsNext.Domain.Common;

/// <summary>
/// Base class for auditable entities with creation and modification tracking.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    public int? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated the entity.
    /// </summary>
    public int? UpdatedBy { get; set; }
}

