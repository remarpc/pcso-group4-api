using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pcso_group4_api.Migrations
{
    public partial class RemoveFrequencyClass2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrequencyViews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FrequencyViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Digit1 = table.Column<long>(type: "bigint", nullable: false),
                    Digit2 = table.Column<long>(type: "bigint", nullable: false),
                    Digit3 = table.Column<long>(type: "bigint", nullable: false),
                    Digit4 = table.Column<long>(type: "bigint", nullable: false),
                    Digit5 = table.Column<long>(type: "bigint", nullable: false),
                    Digit6 = table.Column<long>(type: "bigint", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequencyViews", x => x.Id);
                });
        }
    }
}
