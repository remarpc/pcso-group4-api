using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pcso_group4_api.Migrations
{
    public partial class InitialDbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(type: "int", nullable: false),
                    Digit1 = table.Column<int>(type: "int", nullable: false),
                    Digit2 = table.Column<int>(type: "int", nullable: false),
                    Digit3 = table.Column<int>(type: "int", nullable: false),
                    Digit4 = table.Column<int>(type: "int", nullable: false),
                    Digit5 = table.Column<int>(type: "int", nullable: false),
                    Digit6 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Combinations");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
