using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.src.Contexts.MainDb.Migrations
{
    /// <inheritdoc />
    public partial class Wordsnavigationname7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Name");
        }
    }
}
