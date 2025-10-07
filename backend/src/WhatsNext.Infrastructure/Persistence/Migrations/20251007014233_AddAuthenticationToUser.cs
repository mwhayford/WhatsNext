// <copyright file="20251007014233_AddAuthenticationToUser.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsNext.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddAuthenticationToUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Roles",
            table: "Users",
            type: "nvarchar(500)",
            maxLength: 500,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "Username",
            table: "Users",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.CreateIndex(
            name: "IX_Users_Username",
            table: "Users",
            column: "Username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Users_Username",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "Roles",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "Username",
            table: "Users");
    }
}
