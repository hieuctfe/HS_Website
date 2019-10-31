namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        AvatarName = c.String(nullable: false),
                        AvatarPath = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                        Content = c.String(storeType: "ntext"),
                        PostCategoryId = c.Int(nullable: false),
                        ActivityLog_CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_ModifiedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_CreatedBy = c.String(nullable: false, maxLength: 255),
                        ActivityLog_ModifiedBy = c.String(nullable: false, maxLength: 255),
                        UIOption_DisplayOrder = c.Int(nullable: false),
                        UIOption_IsDisplay = c.Boolean(nullable: false),
                        Seo_Title = c.String(maxLength: 255),
                        Seo_MetaContent = c.String(),
                        Seo_MetaDescription = c.String(),
                        Seo_StructureDate = c.String(),
                        Seo_FriendlyUrl = c.String(),
                        Seo_AliasName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostCategories", t => t.PostCategoryId, cascadeDelete: true)
                .Index(t => t.PostCategoryId);
            
            CreateTable(
                "dbo.PostLabelDatas",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        PostLabelId = c.Int(nullable: false),
                        ActivityLog_CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_ModifiedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_CreatedBy = c.String(nullable: false, maxLength: 255),
                        ActivityLog_ModifiedBy = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => new { t.PostId, t.PostLabelId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.PostLabels", t => t.PostLabelId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.PostLabelId);
            
            CreateTable(
                "dbo.PostLabels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ActivityLog_CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_ModifiedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_CreatedBy = c.String(nullable: false, maxLength: 255),
                        ActivityLog_ModifiedBy = c.String(nullable: false, maxLength: 255),
                        UIOption_DisplayOrder = c.Int(nullable: false),
                        UIOption_IsDisplay = c.Boolean(nullable: false),
                        Seo_Title = c.String(maxLength: 255),
                        Seo_MetaContent = c.String(),
                        Seo_MetaDescription = c.String(),
                        Seo_StructureDate = c.String(),
                        Seo_FriendlyUrl = c.String(),
                        Seo_AliasName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Area = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Int(nullable: false),
                        NumberOfBedRoom = c.Int(nullable: false),
                        NumberOfBathRoom = c.Int(nullable: false),
                        NumberOfGarage = c.Int(nullable: false),
                        HasSwimming = c.Boolean(nullable: false),
                        HasGarden = c.Boolean(nullable: false),
                        HasCarGarage = c.Boolean(nullable: false),
                        Title = c.String(maxLength: 255),
                        ShortDescription = c.String(maxLength: 255),
                        DetailDescription = c.String(storeType: "ntext"),
                        AddressLine = c.String(maxLength: 510),
                        District = c.String(maxLength: 255),
                        City = c.String(maxLength: 255),
                        PropertyStatusCodeId = c.Int(nullable: false),
                        PropertyTypeId = c.Int(nullable: false),
                        ActivityLog_CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_ModifiedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        ActivityLog_CreatedBy = c.String(nullable: false, maxLength: 255),
                        ActivityLog_ModifiedBy = c.String(nullable: false, maxLength: 255),
                        UIOption_DisplayOrder = c.Int(nullable: false),
                        UIOption_IsDisplay = c.Boolean(nullable: false),
                        Seo_Title = c.String(maxLength: 255),
                        Seo_MetaContent = c.String(),
                        Seo_MetaDescription = c.String(),
                        Seo_StructureDate = c.String(),
                        Seo_FriendlyUrl = c.String(),
                        Seo_AliasName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PropertyStatusCodes", t => t.PropertyStatusCodeId, cascadeDelete: true)
                .ForeignKey("dbo.PropertyTypes", t => t.PropertyTypeId, cascadeDelete: true)
                .Index(t => t.PropertyStatusCodeId)
                .Index(t => t.PropertyTypeId);
            
            CreateTable(
                "dbo.PropertyImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(nullable: false),
                        PropertyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PropertyId);
            
            CreateTable(
                "dbo.PropertyStatusCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "PropertyTypeId", "dbo.PropertyTypes");
            DropForeignKey("dbo.Properties", "PropertyStatusCodeId", "dbo.PropertyStatusCodes");
            DropForeignKey("dbo.PropertyImages", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.PostLabelDatas", "PostLabelId", "dbo.PostLabels");
            DropForeignKey("dbo.PostLabelDatas", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "PostCategoryId", "dbo.PostCategories");
            DropForeignKey("dbo.Districts", "CityId", "dbo.Cities");
            DropIndex("dbo.PropertyImages", new[] { "PropertyId" });
            DropIndex("dbo.Properties", new[] { "PropertyTypeId" });
            DropIndex("dbo.Properties", new[] { "PropertyStatusCodeId" });
            DropIndex("dbo.PostLabelDatas", new[] { "PostLabelId" });
            DropIndex("dbo.PostLabelDatas", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "PostCategoryId" });
            DropIndex("dbo.Districts", new[] { "CityId" });
            DropTable("dbo.PropertyTypes");
            DropTable("dbo.PropertyStatusCodes");
            DropTable("dbo.PropertyImages");
            DropTable("dbo.Properties");
            DropTable("dbo.PostLabels");
            DropTable("dbo.PostLabelDatas");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Districts");
            DropTable("dbo.Cities");
        }
    }
}
