namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addViewForPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ViewCount");
        }
    }
}
