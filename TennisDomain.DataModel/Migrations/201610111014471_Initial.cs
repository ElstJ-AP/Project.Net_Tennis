namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerNr = c.Int(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        JoinYear = c.Int(nullable: false),
                        Street = c.String(),
                        HouseNr = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        City = c.String(),
                        PhoneNr = c.String(),
                        FederationNr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        FineDate = c.DateTime(nullable: false),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Player1Id = c.Int(nullable: false),
                        Player2Id = c.Int(nullable: false),
                        Player1Score = c.String(),
                        Player2Score = c.String(),
                        Player_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player1Id)
                .ForeignKey("dbo.Players", t => t.Player2Id)
                .ForeignKey("dbo.Players", t => t.Player_Id)
                .Index(t => t.Player1Id)
                .Index(t => t.Player2Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerDivisions",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.DivisionId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.PlayerRoles",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.RoleId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TeamDivisions",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.DivisionId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.TeamPlayers",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.TeamId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.TeamDivisions", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.TeamDivisions", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.PlayerRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PlayerRoles", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Games", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.Games", "Player2Id", "dbo.Players");
            DropForeignKey("dbo.Games", "Player1Id", "dbo.Players");
            DropForeignKey("dbo.Fines", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerDivisions", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.PlayerDivisions", "PlayerId", "dbo.Players");
            DropIndex("dbo.TeamPlayers", new[] { "TeamId" });
            DropIndex("dbo.TeamPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TeamDivisions", new[] { "DivisionId" });
            DropIndex("dbo.TeamDivisions", new[] { "TeamId" });
            DropIndex("dbo.PlayerRoles", new[] { "RoleId" });
            DropIndex("dbo.PlayerRoles", new[] { "PlayerId" });
            DropIndex("dbo.PlayerDivisions", new[] { "DivisionId" });
            DropIndex("dbo.PlayerDivisions", new[] { "PlayerId" });
            DropIndex("dbo.Games", new[] { "Player_Id" });
            DropIndex("dbo.Games", new[] { "Player2Id" });
            DropIndex("dbo.Games", new[] { "Player1Id" });
            DropIndex("dbo.Fines", new[] { "PlayerId" });
            DropTable("dbo.TeamPlayers");
            DropTable("dbo.TeamDivisions");
            DropTable("dbo.PlayerRoles");
            DropTable("dbo.PlayerDivisions");
            DropTable("dbo.Teams");
            DropTable("dbo.Roles");
            DropTable("dbo.Games");
            DropTable("dbo.Fines");
            DropTable("dbo.Players");
            DropTable("dbo.Divisions");
        }
    }
}
