namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migdeleteprand : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "BrandId", "dbo.ProductBrands");
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropTable("dbo.ProductBrands");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductBrands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandTitle = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateIndex("dbo.Products", "BrandId");
            AddForeignKey("dbo.Products", "BrandId", "dbo.ProductBrands", "BrandId", cascadeDelete: true);
        }
    }
}
