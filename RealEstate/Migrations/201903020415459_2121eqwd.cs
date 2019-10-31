namespace RealEstate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2121eqwd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "CommentId", newName: "ParentId");
            RenameIndex(table: "dbo.Comments", name: "IX_CommentId", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_ParentId", newName: "IX_CommentId");
            RenameColumn(table: "dbo.Comments", name: "ParentId", newName: "CommentId");
        }
    }
}
