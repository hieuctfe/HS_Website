namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class àdv : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Properties", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Properties", "Rating", c => c.Int(nullable: false));
        }
    }
}
