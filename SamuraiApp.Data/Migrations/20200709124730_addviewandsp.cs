using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class addviewandsp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
                ( @"CREATE OR ALTER VIEW dbo.SamuraiBattleStats
          AS
          SELECT dbo.Samurais.Name,
          COUNT(dbo.SamuraiBattle.BattleId) AS NumberOfBattles,
                  dbo.EarliestBattleFoughtBySamurai(MIN(dbo.Samurais.Id)) AS EarliestBattle
          FROM dbo.SamuraiBattle INNER JOIN
               dbo.Samurais ON dbo.SamuraiBattle.SamuraiId = dbo.Samurais.Id
          GROUP BY dbo.Samurais.Name, dbo.SamuraiBattle.SamuraiId"
                );
            migrationBuilder.Sql
                ( @"CREATE PROCEDURE dbo.SamuraisWhoSaidAWord
            @text VARCHAR(20)
            AS
            SELECT      Samurais.Id, Samurais.Name, Samurais.ClanId
            FROM        Samurais INNER JOIN
                        Quotes ON Samurais.Id = Quotes.SamuraiId
            WHERE(Quotes.Text LIKE '%' + @text + '%')"
                );

            migrationBuilder.Sql
                (@"CREATE PROCEDURE dbo.DeleteQuotesForSamurai
             @samuraiId int
             AS
             DELETE FROM Quotes
             WHERE Quotes.SamuraiId = @samuraiId"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Drop VIEW dbo.SamuraiBattleStats");
            migrationBuilder.Sql(@" drop  PROCEDURE dbo.SamuraisWhoSaidAWord");
            migrationBuilder.Sql(@"Drop Procedure dbo.DeleteQuotesForSamurai ");
        }
    }
}
