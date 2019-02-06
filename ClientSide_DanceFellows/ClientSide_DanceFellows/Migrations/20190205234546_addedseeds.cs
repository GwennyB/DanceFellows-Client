using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSide_DanceFellows.Migrations
{
    public partial class addedseeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Competitions",
                columns: new[] { "ID", "CompType", "Level" },
                values: new object[] { 1, 2, 1 });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ID", "FirstName", "LastName", "MaxLevel", "MinLevel", "WSC_ID" },
                values: new object[] { 1, "JimBob", "Franklin", 3, 1, 1234 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Competitions",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
