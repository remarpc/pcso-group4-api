using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pcso_group4_api.Migrations
{
    public partial class AddFrequencyView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[Frequency] AS
									SELECT row_number() OVER (ORDER BY GameID) AS Id, A.* FROM 
									(
										SELECT GameID, Digit, CONCAT('Digit',RN) AS RN
										FROM 
										(
											SELECT GameID, Digit , COUNT(Digit) AS 'Value Occurence', ROW_NUMBER() OVER(PARTITION BY GameID ORDER BY COUNT(Digit) DESC) AS RN
											FROM 
											(
												SELECT GameID,Digit1,Digit2,Digit3,Digit4,Digit5,Digit6
												FROM Combinations
											) A
											UNPIVOT (
												Digit FOR Digits in(Digit1,Digit2,Digit3,Digit4,Digit5,Digit6)
											) UP
											GROUP BY Digit, GameID
										) A
										WHERE RN <= 6
									) A
									PIVOT
									(
										MAX(Digit)
										FOR RN IN ([Digit1],[Digit2],[Digit3],[Digit4],[Digit5],[Digit6])
									)A");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW [dbo].[Frequency];");
        }
    }
}
