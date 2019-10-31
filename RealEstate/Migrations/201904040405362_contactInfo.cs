namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "ContactName", c => c.String(maxLength: 255));
            AddColumn("dbo.Properties", "ContactAddress", c => c.String(maxLength: 510));
            AddColumn("dbo.Properties", "ContactPhoneNumber", c => c.String());
            AddColumn("dbo.Properties", "ContactEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "ContactEmail");
            DropColumn("dbo.Properties", "ContactPhoneNumber");
            DropColumn("dbo.Properties", "ContactAddress");
            DropColumn("dbo.Properties", "ContactName");
        }
    }
}
