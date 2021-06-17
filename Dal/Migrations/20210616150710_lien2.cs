using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class lien2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_People_PersonId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PersonId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PersonId",
                table: "Messages",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_People_PersonId",
                table: "Messages",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_People_PersonId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PersonId",
                table: "Messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PersonId1",
                table: "Messages",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_People_PersonId1",
                table: "Messages",
                column: "PersonId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
