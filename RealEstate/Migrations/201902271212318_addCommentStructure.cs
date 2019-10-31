namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCommentStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Owner = c.String(nullable: false, maxLength: 255),
                        EmailAddress = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 1000),
                        IsVerify = c.Boolean(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        PostId = c.Int(),
                        PropertyId = c.Int(),
                        CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Properties", t => t.PropertyId)
                .Index(t => t.PostId)
                .Index(t => t.PropertyId)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "CommentId", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "PropertyId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropTable("dbo.Comments");
        }
    }
}
