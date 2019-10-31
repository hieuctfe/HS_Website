namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "AvatarName", c => c.String());
            AlterColumn("dbo.Properties", "AvatarPath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "AvatarPath", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "AvatarName", c => c.String(nullable: false));
        }
    }
}
