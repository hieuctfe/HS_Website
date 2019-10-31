namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdsv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "OrderStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "OrderStatus");
        }
    }
}
