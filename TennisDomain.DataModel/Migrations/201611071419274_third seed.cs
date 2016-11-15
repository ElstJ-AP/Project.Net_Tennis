namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdseed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "Birthday", c => c.DateTime());
        }
    }
}
