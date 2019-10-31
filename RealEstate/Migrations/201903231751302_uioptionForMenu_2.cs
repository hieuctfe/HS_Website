namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uioptionForMenu_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostCategories", "UIOption_DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.PostCategories", "UIOption_IsDisplay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostCategories", "UIOption_IsDisplay");
            DropColumn("dbo.PostCategories", "UIOption_DisplayOrder");
        }
    }
}
