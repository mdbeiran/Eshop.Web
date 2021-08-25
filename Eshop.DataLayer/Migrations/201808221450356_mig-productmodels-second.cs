namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migproductmodelssecond : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductGroup_GroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.Products", "MainGroupId", "dbo.ProductGroups");
            DropIndex("dbo.Products", new[] { "SubGroupId" });
            DropIndex("dbo.Products", new[] { "ProductGroup_GroupId" });
            AlterColumn("dbo.Products", "SubGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "SubGroupId");
            AddForeignKey("dbo.Products", "MainGroupId", "dbo.ProductGroups", "GroupId");
            DropColumn("dbo.Products", "ProductGroup_GroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductGroup_GroupId", c => c.Int());
            DropForeignKey("dbo.Products", "MainGroupId", "dbo.ProductGroups");
            DropIndex("dbo.Products", new[] { "SubGroupId" });
            AlterColumn("dbo.Products", "SubGroupId", c => c.Int());
            CreateIndex("dbo.Products", "ProductGroup_GroupId");
            CreateIndex("dbo.Products", "SubGroupId");
            AddForeignKey("dbo.Products", "MainGroupId", "dbo.ProductGroups", "GroupId", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ProductGroup_GroupId", "dbo.ProductGroups", "GroupId");
        }
    }
}
