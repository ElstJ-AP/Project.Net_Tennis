namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "JoinYear", c => c.Int());
            AlterColumn("dbo.Players", "HouseNr", c => c.Int());
            AlterColumn("dbo.Players", "FederationNr", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "FederationNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "HouseNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "JoinYear", c => c.Int(nullable: false));
        }
    }
}
