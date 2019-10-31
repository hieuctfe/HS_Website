namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uioptionForMenu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "ParentId", "dbo.Menus");
            DropIndex("dbo.Menus", new[] { "ParentId" });
            AddColumn("dbo.PropertyStatusCodes", "UIOption_DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.PropertyStatusCodes", "UIOption_IsDisplay", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropertyTypes", "UIOption_DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.PropertyTypes", "UIOption_IsDisplay", c => c.Boolean(nullable: false));
            DropTable("dbo.Menus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Url = c.String(nullable: false, maxLength: 255),
                        ParentId = c.Int(),
                        UIOption_DisplayOrder = c.Int(nullable: false),
                        UIOption_IsDisplay = c.Boolean(nullable: false),
                        Seo_Title = c.String(maxLength: 255),
                        Seo_MetaContent = c.String(),
                        Seo_MetaDescription = c.String(),
                        Seo_StructureData = c.String(),
                        Seo_FriendlyUrl = c.String(),
                        Seo_AliasName = c.String(),
                        ActivityLog_CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_ModifiedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_CreatedBy = c.String(nullable: false, maxLength: 255),
                        ActivityLog_ModifiedBy = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.PropertyTypes", "UIOption_IsDisplay");
            DropColumn("dbo.PropertyTypes", "UIOption_DisplayOrder");
            DropColumn("dbo.PropertyStatusCodes", "UIOption_IsDisplay");
            DropColumn("dbo.PropertyStatusCodes", "UIOption_DisplayOrder");
            CreateIndex("dbo.Menus", "ParentId");
            AddForeignKey("dbo.Menus", "ParentId", "dbo.Menus", "Id");
        }
    }
}
