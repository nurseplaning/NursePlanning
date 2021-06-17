using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class lien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "People",
                newName: "BirthDay");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Messages",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtHome = table.Column<bool>(type: "bit", nullable: false),
                    NurseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Nurses_NurseId1",
                        column: x => x.NurseId1,
                        principalTable: "Nurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId1",
                        column: x => x.PatientId1,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AppointmentId",
                table: "Messages",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PersonId1",
                table: "Messages",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_NurseId1",
                table: "Appointments",
                column: "NurseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId1",
                table: "Appointments",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StatusId",
                table: "Appointments",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Appointments_AppointmentId",
                table: "Messages",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_People_PersonId1",
                table: "Messages",
                column: "PersonId1",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Appointments_AppointmentId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_People_PersonId1",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AppointmentId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PersonId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "People",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "MessageId");
        }
    }
}
