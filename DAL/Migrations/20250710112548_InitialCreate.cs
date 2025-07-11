﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Password = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    BloodGroup = table.Column<string>(type: "varchar", maxLength: 20, nullable: true),
                    UserType = table.Column<string>(type: "varchar", maxLength: 50, nullable: true),
                    ProfilePicture = table.Column<string>(type: "varchar", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
