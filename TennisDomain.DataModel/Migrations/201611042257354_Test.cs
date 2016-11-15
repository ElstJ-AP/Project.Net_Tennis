namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerDivisions", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerDivisions", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.TeamDivisions", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamDivisions", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams");
            DropIndex("dbo.PlayerDivisions", new[] { "PlayerId" });
            DropIndex("dbo.PlayerDivisions", new[] { "DivisionId" });
            DropIndex("dbo.TeamDivisions", new[] { "TeamId" });
            DropIndex("dbo.TeamDivisions", new[] { "DivisionId" });
            DropIndex("dbo.TeamPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TeamPlayers", new[] { "TeamId" });
            CreateTable(
                "dbo.PlayerTeamDivisions",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.TeamId, t.DivisionId })
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.TeamId)
                .Index(t => t.DivisionId);
            
            DropTable("dbo.PlayerDivisions");
            DropTable("dbo.TeamDivisions");
            DropTable("dbo.TeamPlayers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamPlayers",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.TeamId });
            
            CreateTable(
                "dbo.TeamDivisions",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.DivisionId });
            
            CreateTable(
                "dbo.PlayerDivisions",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.DivisionId });
            
            DropForeignKey("dbo.PlayerTeamDivisions", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.PlayerTeamDivisions", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerTeamDivisions", "DivisionId", "dbo.Divisions");
            DropIndex("dbo.PlayerTeamDivisions", new[] { "DivisionId" });
            DropIndex("dbo.PlayerTeamDivisions", new[] { "TeamId" });
            DropIndex("dbo.PlayerTeamDivisions", new[] { "PlayerId" });
            DropTable("dbo.PlayerTeamDivisions");
            CreateIndex("dbo.TeamPlayers", "TeamId");
            CreateIndex("dbo.TeamPlayers", "PlayerId");
            CreateIndex("dbo.TeamDivisions", "DivisionId");
            CreateIndex("dbo.TeamDivisions", "TeamId");
            CreateIndex("dbo.PlayerDivisions", "DivisionId");
            CreateIndex("dbo.PlayerDivisions", "PlayerId");
            AddForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamDivisions", "DivisionId", "dbo.Divisions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamDivisions", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlayerDivisions", "DivisionId", "dbo.Divisions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlayerDivisions", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
