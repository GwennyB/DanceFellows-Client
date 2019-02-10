using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSide_DanceFellows.Migrations
{
    public partial class updateddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ID",
                keyValue: 1,
                column: "EligibleCompetitor",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Participants",
                keyColumn: "ID",
                keyValue: 1,
                column: "EligibleCompetitor",
                value: false);
        }
    }
}
