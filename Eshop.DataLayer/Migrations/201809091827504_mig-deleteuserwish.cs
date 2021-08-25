namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migdeleteuserwish : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserWishes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.UserWishes", "UserId", "dbo.Users");
            DropIndex("dbo.UserWishes", new[] { "UserId" });
            DropIndex("dbo.UserWishes", new[] { "ProductId" });
            DropTable("dbo.UserWishes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserWishes",
                c => new
                    {
                        WishId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WishId);
            
            CreateIndex("dbo.UserWishes", "ProductId");
            CreateIndex("dbo.UserWishes", "UserId");
            AddForeignKey("dbo.UserWishes", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserWishes", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
