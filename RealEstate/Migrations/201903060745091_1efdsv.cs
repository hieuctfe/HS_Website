namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1efdsv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UICaches",
                c => new
                    {
                        PageName = c.String(nullable: false, maxLength: 255),
                        ComponentName = c.String(nullable: false, maxLength: 255),
                        DataType = c.String(nullable: false, maxLength: 20),
                        DataCache = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => new { t.PageName, t.ComponentName, t.DataType });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UICaches");
        }
    }
}
