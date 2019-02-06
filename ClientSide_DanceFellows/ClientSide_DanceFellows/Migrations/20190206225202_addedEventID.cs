using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSide_DanceFellows.Migrations
{
    public partial class addedEventID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "RegisteredCompetitors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventID",
                table: "RegisteredCompetitors");
        }
    }
}
