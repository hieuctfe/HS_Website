namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "AvatarName", c => c.String(nullable: false));
            AddColumn("dbo.Properties", "AvatarPath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "AvatarPath");
            DropColumn("dbo.Properties", "AvatarName");
        }
    }
}
