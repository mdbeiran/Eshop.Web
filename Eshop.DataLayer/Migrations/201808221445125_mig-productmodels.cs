namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migproductmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 20),
                        Comment = c.String(nullable: false, maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductComments", t => t.ParentId)
                .Index(t => t.ProductId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        MainGroupId = c.Int(nullable: false),
                        SubGroupId = c.Int(),
                        ProductCode = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 360),
                        ShortDescription = c.String(nullable: false, maxLength: 360),
                        SingleProductImageName = c.String(maxLength: 500),
                        PUblicProductImageName = c.String(maxLength: 500),
                        Text = c.String(nullable: false),
                        SinglePrice = c.Int(nullable: false),
                        PublicPrice = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsExist = c.Boolean(nullable: false),
                        ProductGroup_GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup_GroupId)
                .ForeignKey("dbo.ProductGroups", t => t.MainGroupId, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.SubGroupId)
                .Index(t => t.MainGroupId)
                .Index(t => t.SubGroupId)
                .Index(t => t.ProductGroup_GroupId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupTitle = c.String(nullable: false, maxLength: 200),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.ProductGroups", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.ProductGalleries",
                c => new
                    {
                        GalleryId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.GalleryId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductSelectedFeatures",
                c => new
                    {
                        PF_Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        FeatureId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.PF_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductFeatures", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.FeatureId);
            
            CreateTable(
                "dbo.ProductFeatures",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        FeatureTitle = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.FeatureId);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        TagTitle = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductComments", "ParentId", "dbo.ProductComments");
            DropForeignKey("dbo.Products", "SubGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductTags", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductSelectedFeatures", "FeatureId", "dbo.ProductFeatures");
            DropForeignKey("dbo.ProductSelectedFeatures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductGalleries", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "MainGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.Products", "ProductGroup_GroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ParentId", "dbo.ProductGroups");
            DropIndex("dbo.ProductTags", new[] { "ProductId" });
            DropIndex("dbo.ProductSelectedFeatures", new[] { "FeatureId" });
            DropIndex("dbo.ProductSelectedFeatures", new[] { "ProductId" });
            DropIndex("dbo.ProductGalleries", new[] { "ProductId" });
            DropIndex("dbo.ProductGroups", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "ProductGroup_GroupId" });
            DropIndex("dbo.Products", new[] { "SubGroupId" });
            DropIndex("dbo.Products", new[] { "MainGroupId" });
            DropIndex("dbo.ProductComments", new[] { "ParentId" });
            DropIndex("dbo.ProductComments", new[] { "ProductId" });
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductFeatures");
            DropTable("dbo.ProductSelectedFeatures");
            DropTable("dbo.ProductGalleries");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.Products");
            DropTable("dbo.ProductComments");
        }
    }
}
