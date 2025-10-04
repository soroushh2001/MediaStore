using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediaStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "IsDeleted", "LastModifiedDate", "ParentId", "Slug", "Title" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "tv", "تلویزیون" },
                    { 2, null, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "full-hd", "FullHd" },
                    { 3, null, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "4k", "4K" },
                    { 4, null, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ref", "یخچال" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
