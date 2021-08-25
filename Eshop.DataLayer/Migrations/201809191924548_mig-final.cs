namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migfinal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderSelectedFeatures",
                c => new
                    {
                        OrderSelectedId = c.Int(nullable: false, identity: true),
                        OrderDetailId = c.Int(nullable: false),
                        PF_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderSelectedId)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetailId, cascadeDelete: true)
                .ForeignKey("dbo.ProductSelectedFeatures", t => t.PF_Id)
                .Index(t => t.OrderDetailId)
                .Index(t => t.PF_Id);
            
            AddColumn("dbo.SiteSettings", "PublicProductCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderSelectedFeatures", "PF_Id", "dbo.ProductSelectedFeatures");
            DropForeignKey("dbo.OrderSelectedFeatures", "OrderDetailId", "dbo.OrderDetails");
            DropIndex("dbo.OrderSelectedFeatures", new[] { "PF_Id" });
            DropIndex("dbo.OrderSelectedFeatures", new[] { "OrderDetailId" });
            DropColumn("dbo.SiteSettings", "PublicProductCount");
            DropTable("dbo.OrderSelectedFeatures");
        }
    }
}
