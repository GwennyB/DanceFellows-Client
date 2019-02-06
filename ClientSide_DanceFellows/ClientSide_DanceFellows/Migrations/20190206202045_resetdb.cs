using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSide_DanceFellows.Migrations
{
    public partial class resetdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
