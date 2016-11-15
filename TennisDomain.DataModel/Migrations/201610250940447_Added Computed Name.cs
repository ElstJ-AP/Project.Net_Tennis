namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedComputedName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Players", "FirstName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Players", "LastName", c => c.String(maxLength: 50));
        }
    }
}
