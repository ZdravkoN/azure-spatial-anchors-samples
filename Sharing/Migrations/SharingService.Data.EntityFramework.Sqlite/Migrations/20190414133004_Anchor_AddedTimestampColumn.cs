﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SharingService.Data.EntityFramework.Sqlite.Migrations
{
    public partial class Anchor_AddedTimestampColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "anchors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "anchors");
        }
    }
}
