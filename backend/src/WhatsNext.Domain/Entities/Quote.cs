// <copyright file="Quote.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using WhatsNext.Domain.Common;

namespace WhatsNext.Domain.Entities;

/// <summary>
/// Represents a motivational quote.
/// </summary>
public class Quote : BaseEntity
{
    /// <summary>
    /// Gets or sets the text of the quote.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the author of the quote.
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the quote.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the source of the quote.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the collection of users who have favorited this quote.
    /// </summary>
    public ICollection<User> FavoritedByUsers { get; set; } = new List<User>();
}
