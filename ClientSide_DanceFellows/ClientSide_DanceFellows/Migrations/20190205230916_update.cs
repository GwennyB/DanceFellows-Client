using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSide_DanceFellows.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegisteredCompetitors_ParticipantID",
                table: "RegisteredCompetitors");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCompetitors_ParticipantID",
                table: "RegisteredCompetitors",
                column: "ParticipantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RegisteredCompetitors_ParticipantID",
                table: "RegisteredCompetitors");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCompetitors_ParticipantID",
                table: "RegisteredCompetitors",
                column: "ParticipantID",
                unique: true);
        }
    }
}
