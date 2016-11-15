namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gamesplayerfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Player_Id", "dbo.Players");
            DropIndex("dbo.Games", new[] { "Player_Id" });
            DropColumn("dbo.Games", "Player_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Player_Id", c => c.Int());
            CreateIndex("dbo.Games", "Player_Id");
            AddForeignKey("dbo.Games", "Player_Id", "dbo.Players", "Id");
        }
    }
}
