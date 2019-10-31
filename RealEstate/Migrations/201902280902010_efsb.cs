namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class efsb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "HeaderImageName", c => c.String());
            AddColumn("dbo.Posts", "HeaderImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "HeaderImagePath");
            DropColumn("dbo.Posts", "HeaderImageName");
        }
    }
}
