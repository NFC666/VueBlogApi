using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VueBlog.API.Migrations
{
    /// <inheritdoc />
    public partial class TEST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Essays");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
