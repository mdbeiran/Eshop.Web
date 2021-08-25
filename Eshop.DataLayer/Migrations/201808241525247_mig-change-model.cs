namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductComments", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductComments", "UserID");
            AddForeignKey("dbo.ProductComments", "UserID", "dbo.Users", "UserId");
            DropColumn("dbo.ProductComments", "Name");
            DropColumn("dbo.ProductComments", "Mobile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductComments", "Mobile", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.ProductComments", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.ProductComments", "UserID", "dbo.Users");
            DropIndex("dbo.ProductComments", new[] { "UserID" });
            DropColumn("dbo.ProductComments", "UserID");
        }
    }
}
