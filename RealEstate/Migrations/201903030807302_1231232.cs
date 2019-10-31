namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1231232 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PostCategories", "Name", unique: true);
            CreateIndex("dbo.PostLabels", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PostLabels", new[] { "Name" });
            DropIndex("dbo.PostCategories", new[] { "Name" });
        }
    }
}
