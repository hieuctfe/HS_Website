namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priceNotAllowNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Properties", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
