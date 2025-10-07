// <copyright file="User.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using WhatsNext.Domain.Common;

namespace WhatsNext.Domain.Entities;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's hashed password.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the user's full name.
    /// </summary>
    public string FullName => $"{this.FirstName} {this.LastName}".Trim();

    /// <summary>
    /// Gets or sets a value indicating whether the user's email is confirmed.
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the user's refresh token for JWT authentication.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the expiry date of the refresh token.
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    /// <summary>
    /// Gets or sets the user's timezone.
    /// </summary>
    public string TimeZone { get; set; } = "UTC";

    /// <summary>
    /// Gets or sets the collection of habits associated with this user.
    /// </summary>
    public ICollection<Habit> Habits { get; set; } = new List<Habit>();

    /// <summary>
    /// Gets or sets the collection of tasks associated with this user.
    /// </summary>
    public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();

    /// <summary>
    /// Gets or sets the collection of timer sessions associated with this user.
    /// </summary>
    public ICollection<TimerSession> TimerSessions { get; set; } = new List<TimerSession>();

    /// <summary>
    /// Gets or sets the collection of favorite quotes associated with this user.
    /// </summary>
    public ICollection<Quote> FavoriteQuotes { get; set; } = new List<Quote>();
}
