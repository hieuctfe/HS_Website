namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customTreeMenu : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "ParentId", "dbo.Menus");
            DropIndex("dbo.Menus", new[] { "ParentId" });
            DropTable("dbo.Menus");
        }
    }
}
