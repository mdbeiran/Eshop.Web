namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migproductmodelsthird : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductBrands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandTitle = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.BrandId);
            
            AddColumn("dbo.Products", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "BrandId");
            AddForeignKey("dbo.Products", "BrandId", "dbo.ProductBrands", "BrandId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "BrandId", "dbo.ProductBrands");
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropColumn("dbo.Products", "BrandId");
            DropTable("dbo.ProductBrands");
        }
    }
}
