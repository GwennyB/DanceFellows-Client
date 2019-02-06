using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSide_DanceFellows.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompType = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WSC_ID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MinLevel = table.Column<int>(nullable: false),
                    MaxLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredCompetitors",
                columns: table => new
                {
                    ParticipantID = table.Column<int>(nullable: false),
                    CompetitionID = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Placement = table.Column<int>(nullable: false),
                    BibNumber = table.Column<int>(nullable: false),
                    ChiefJudgeScore = table.Column<int>(nullable: false),
                    JudgeOneScore = table.Column<int>(nullable: false),
                    JudgeTwoScore = table.Column<int>(nullable: false),
                    JudgeThreeScore = table.Column<int>(nullable: false),
                    JudgeFourScore = table.Column<int>(nullable: false),
                    JudgeFiveScore = table.Column<int>(nullable: false),
                    JudgeSixScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCompetitors", x => new { x.CompetitionID, x.ParticipantID });
                    table.ForeignKey(
                        name: "FK_RegisteredCompetitors_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisteredCompetitors_Participants_ParticipantID",
                        column: x => x.ParticipantID,
                        principalTable: "Participants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCompetitors_ParticipantID",
                table: "RegisteredCompetitors",
                column: "ParticipantID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredCompetitors");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Participants");
        }
    }
}
