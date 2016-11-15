namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Annotations_Added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Divisions", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Players", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Players", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Players", "Street", c => c.String(maxLength: 30));
            AlterColumn("dbo.Players", "ZipCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.Players", "City", c => c.String(maxLength: 30));
            AlterColumn("dbo.Players", "PhoneNr", c => c.String(maxLength: 15));
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teams", "Name", c => c.String());
            AlterColumn("dbo.Players", "PhoneNr", c => c.String());
            AlterColumn("dbo.Players", "City", c => c.String());
            AlterColumn("dbo.Players", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "Street", c => c.String());
            AlterColumn("dbo.Players", "FirstName", c => c.String());
            AlterColumn("dbo.Players", "LastName", c => c.String());
            AlterColumn("dbo.Divisions", "Name", c => c.String());
        }
    }
}
