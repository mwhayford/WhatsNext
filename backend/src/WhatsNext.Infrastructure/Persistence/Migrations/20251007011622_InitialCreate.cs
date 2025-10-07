// <copyright file="20251007011622_InitialCreate.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsNext.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Quotes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Source = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Quotes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                RefreshToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                TimeZone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "UTC"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Habits",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Frequency = table.Column<int>(type: "int", nullable: false),
                Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "#3B82F6"),
                Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                TargetCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                CurrentStreak = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                LongestStreak = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                CreatedBy = table.Column<int>(type: "int", nullable: true),
                UpdatedBy = table.Column<int>(type: "int", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Habits", x => x.Id);
                table.ForeignKey(
                    name: "FK_Habits_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Tasks",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                Priority = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsImportant = table.Column<bool>(type: "bit", nullable: false),
                Tags = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                CreatedBy = table.Column<int>(type: "int", nullable: true),
                UpdatedBy = table.Column<int>(type: "int", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tasks", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tasks_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TimerSessions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                SessionType = table.Column<int>(type: "int", nullable: false),
                DurationMinutes = table.Column<int>(type: "int", nullable: false),
                StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TimerSessions", x => x.Id);
                table.ForeignKey(
                    name: "FK_TimerSessions_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserFavoriteQuotes",
            columns: table => new
            {
                FavoriteQuotesId = table.Column<int>(type: "int", nullable: false),
                FavoritedByUsersId = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserFavoriteQuotes", x => new { x.FavoriteQuotesId, x.FavoritedByUsersId });
                table.ForeignKey(
                    name: "FK_UserFavoriteQuotes_Quotes_FavoriteQuotesId",
                    column: x => x.FavoriteQuotesId,
                    principalTable: "Quotes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserFavoriteQuotes_Users_FavoritedByUsersId",
                    column: x => x.FavoritedByUsersId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "HabitCompletions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                HabitId = table.Column<int>(type: "int", nullable: false),
                CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HabitCompletions", x => x.Id);
                table.ForeignKey(
                    name: "FK_HabitCompletions_Habits_HabitId",
                    column: x => x.HabitId,
                    principalTable: "Habits",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_HabitCompletions_CompletedDate",
            table: "HabitCompletions",
            column: "CompletedDate");

        migrationBuilder.CreateIndex(
            name: "IX_HabitCompletions_HabitId",
            table: "HabitCompletions",
            column: "HabitId");

        migrationBuilder.CreateIndex(
            name: "IX_HabitCompletions_HabitId_CompletedDate",
            table: "HabitCompletions",
            columns: new[] { "HabitId", "CompletedDate" });

        migrationBuilder.CreateIndex(
            name: "IX_Habits_IsActive",
            table: "Habits",
            column: "IsActive");

        migrationBuilder.CreateIndex(
            name: "IX_Habits_StartDate",
            table: "Habits",
            column: "StartDate");

        migrationBuilder.CreateIndex(
            name: "IX_Habits_UserId",
            table: "Habits",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Quotes_Author",
            table: "Quotes",
            column: "Author");

        migrationBuilder.CreateIndex(
            name: "IX_Quotes_Category",
            table: "Quotes",
            column: "Category");

        migrationBuilder.CreateIndex(
            name: "IX_Tasks_DueDate",
            table: "Tasks",
            column: "DueDate");

        migrationBuilder.CreateIndex(
            name: "IX_Tasks_IsImportant",
            table: "Tasks",
            column: "IsImportant");

        migrationBuilder.CreateIndex(
            name: "IX_Tasks_Priority",
            table: "Tasks",
            column: "Priority");

        migrationBuilder.CreateIndex(
            name: "IX_Tasks_Status",
            table: "Tasks",
            column: "Status");

        migrationBuilder.CreateIndex(
            name: "IX_Tasks_UserId",
            table: "Tasks",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_TimerSessions_IsCompleted",
            table: "TimerSessions",
            column: "IsCompleted");

        migrationBuilder.CreateIndex(
            name: "IX_TimerSessions_SessionType",
            table: "TimerSessions",
            column: "SessionType");

        migrationBuilder.CreateIndex(
            name: "IX_TimerSessions_StartTime",
            table: "TimerSessions",
            column: "StartTime");

        migrationBuilder.CreateIndex(
            name: "IX_TimerSessions_UserId",
            table: "TimerSessions",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserFavoriteQuotes_FavoritedByUsersId",
            table: "UserFavoriteQuotes",
            column: "FavoritedByUsersId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "HabitCompletions");

        migrationBuilder.DropTable(
            name: "Tasks");

        migrationBuilder.DropTable(
            name: "TimerSessions");

        migrationBuilder.DropTable(
            name: "UserFavoriteQuotes");

        migrationBuilder.DropTable(
            name: "Habits");

        migrationBuilder.DropTable(
            name: "Quotes");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
