using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediaStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "LastModifiedDate", "Slug", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "samsung", "سامسونگ" },
                    { 2, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "lg", "ال جی" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
