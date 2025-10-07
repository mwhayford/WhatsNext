// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WhatsNext.Domain.Entities;
using WhatsNext.Infrastructure.Persistence.Contexts;

namespace WhatsNext.Infrastructure.Persistence.Seeders;

/// <summary>
/// Seeds initial data into the database.
/// </summary>
public class DataSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DataSeeder> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataSeeder"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DataSeeder(ApplicationDbContext context, ILogger<DataSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SeedAsync()
    {
        try
        {
            // Ensure database is created
            await _context.Database.MigrateAsync();

            // Seed quotes if none exist
            if (!await _context.Quotes.AnyAsync())
            {
                await SeedQuotesAsync();
            }

            _logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task SeedQuotesAsync()
    {
        var quotes = new List<Quote>
        {
            new Quote
            {
                Text = "The only way to do great work is to love what you do.",
                Author = "Steve Jobs",
                Category = "Motivation",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "Success is not final, failure is not fatal: it is the courage to continue that counts.",
                Author = "Winston Churchill",
                Category = "Motivation",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "Believe you can and you're halfway there.",
                Author = "Theodore Roosevelt",
                Category = "Motivation",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "The future depends on what you do today.",
                Author = "Mahatma Gandhi",
                Category = "Inspiration",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "Don't watch the clock; do what it does. Keep going.",
                Author = "Sam Levenson",
                Category = "Productivity",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "The way to get started is to quit talking and begin doing.",
                Author = "Walt Disney",
                Category = "Productivity",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "Success is the sum of small efforts repeated day in and day out.",
                Author = "Robert Collier",
                Category = "Habits",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "You don't have to be great to start, but you have to start to be great.",
                Author = "Zig Ziglar",
                Category = "Motivation",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "The only impossible journey is the one you never begin.",
                Author = "Tony Robbins",
                Category = "Inspiration",
                CreatedAt = DateTime.UtcNow,
            },
            new Quote
            {
                Text = "Your time is limited, don't waste it living someone else's life.",
                Author = "Steve Jobs",
                Category = "Life",
                CreatedAt = DateTime.UtcNow,
            },
        };

        await _context.Quotes.AddRangeAsync(quotes);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Seeded {Count} quotes", quotes.Count);
    }
}

