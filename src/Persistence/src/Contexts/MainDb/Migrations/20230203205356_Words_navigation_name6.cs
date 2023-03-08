using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.src.Contexts.MainDb.Migrations
{
    /// <inheritdoc />
    public partial class Wordsnavigationname6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Words_WordId",
                table: "Answers");

            migrationBuilder.AlterColumn<Guid>(
                name: "WordId",
                table: "Answers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Words_WordId",
                table: "Answers",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Words_WordId",
                table: "Answers");

            migrationBuilder.AlterColumn<Guid>(
                name: "WordId",
                table: "Answers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Words_WordId",
                table: "Answers",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id");
        }
    }
}
