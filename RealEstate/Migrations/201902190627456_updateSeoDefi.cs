namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSeoDefi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Seo_StructureData", c => c.String());
            AddColumn("dbo.PostLabels", "Seo_StructureData", c => c.String());
            AddColumn("dbo.Properties", "Seo_StructureData", c => c.String());
            DropColumn("dbo.Posts", "Seo_StructureDate");
            DropColumn("dbo.PostLabels", "Seo_StructureDate");
            DropColumn("dbo.Properties", "Seo_StructureDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Properties", "Seo_StructureDate", c => c.String());
            AddColumn("dbo.PostLabels", "Seo_StructureDate", c => c.String());
            AddColumn("dbo.Posts", "Seo_StructureDate", c => c.String());
            DropColumn("dbo.Properties", "Seo_StructureData");
            DropColumn("dbo.PostLabels", "Seo_StructureData");
            DropColumn("dbo.Posts", "Seo_StructureData");
        }
    }
}
