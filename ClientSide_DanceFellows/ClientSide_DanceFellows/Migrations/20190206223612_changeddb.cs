using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSide_DanceFellows.Migrations
{
    public partial class changeddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Participants_ParticipantID",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantID",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ParticipantID",
                table: "Participants");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Participants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Participants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EligibleCompetitor",
                table: "Participants",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EligibleCompetitor",
                table: "Participants");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Participants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Participants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ParticipantID",
                table: "Participants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ParticipantID",
                table: "Participants",
                column: "ParticipantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Participants_ParticipantID",
                table: "Participants",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
