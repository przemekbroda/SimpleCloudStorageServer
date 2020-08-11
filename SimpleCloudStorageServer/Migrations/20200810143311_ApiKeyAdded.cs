using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCloudStorageServer.Migrations
{
    public partial class ApiKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "user");

            migrationBuilder.AddColumn<byte[]>(
                name: "ApiKeyHash",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ApiKeySalt",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppId",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionAt",
                table: "user",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MainDirectory",
                table: "user",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKeyHash",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ApiKeySalt",
                table: "user");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "DeletionAt",
                table: "user");

            migrationBuilder.DropColumn(
                name: "MainDirectory",
                table: "user");

            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "user",
                type: "text",
                nullable: true);
        }
    }
}
