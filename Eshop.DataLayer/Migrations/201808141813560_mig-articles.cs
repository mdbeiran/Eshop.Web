namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migarticles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleComments",
                c => new
                    {
                        CommantID = c.Int(nullable: false, identity: true),
                        ArticleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 800),
                        CreateDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.CommantID)
                .ForeignKey("dbo.Articles", t => t.ArticleID, cascadeDelete: true)
                .ForeignKey("dbo.ArticleComments", t => t.ParentID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.ArticleID)
                .Index(t => t.UserID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        AuthorID = c.Int(nullable: false),
                        ArticleTitle = c.String(nullable: false, maxLength: 400),
                        ShortDescription = c.String(nullable: false, maxLength: 500),
                        ArticleText = c.String(nullable: false),
                        ArticleImageName = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        IsVip = c.Boolean(nullable: false),
                        CanInsertComment = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Users", t => t.AuthorID, cascadeDelete: true)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        ArticleCategoryID = c.Int(nullable: false, identity: true),
                        ArticleID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleCategoryID)
                .ForeignKey("dbo.Articles", t => t.ArticleID, cascadeDelete: true)
                .ForeignKey("dbo.ArticleCategories1", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.ArticleID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ArticleCategories1",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 260),
                        NameInUrl = c.String(nullable: false, maxLength: 200),
                        ParentID = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.ArticleCategories1", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.ArticleTags",
                c => new
                    {
                        ArticleTagID = c.Int(nullable: false, identity: true),
                        ArticleID = c.Int(nullable: false),
                        Tag = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ArticleTagID)
                .ForeignKey("dbo.Articles", t => t.ArticleID, cascadeDelete: true)
                .Index(t => t.ArticleID);
            
            CreateTable(
                "dbo.ArticleVisits",
                c => new
                    {
                        VisitID = c.Int(nullable: false, identity: true),
                        ArticleID = c.Int(nullable: false),
                        IP = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.VisitID)
                .ForeignKey("dbo.Articles", t => t.ArticleID, cascadeDelete: true)
                .Index(t => t.ArticleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleComments", "UserID", "dbo.Users");
            DropForeignKey("dbo.ArticleComments", "ParentID", "dbo.ArticleComments");
            DropForeignKey("dbo.Articles", "AuthorID", "dbo.Users");
            DropForeignKey("dbo.ArticleVisits", "ArticleID", "dbo.Articles");
            DropForeignKey("dbo.ArticleTags", "ArticleID", "dbo.Articles");
            DropForeignKey("dbo.ArticleComments", "ArticleID", "dbo.Articles");
            DropForeignKey("dbo.ArticleCategories", "CategoryID", "dbo.ArticleCategories1");
            DropForeignKey("dbo.ArticleCategories1", "ParentID", "dbo.ArticleCategories1");
            DropForeignKey("dbo.ArticleCategories", "ArticleID", "dbo.Articles");
            DropIndex("dbo.ArticleVisits", new[] { "ArticleID" });
            DropIndex("dbo.ArticleTags", new[] { "ArticleID" });
            DropIndex("dbo.ArticleCategories1", new[] { "ParentID" });
            DropIndex("dbo.ArticleCategories", new[] { "CategoryID" });
            DropIndex("dbo.ArticleCategories", new[] { "ArticleID" });
            DropIndex("dbo.Articles", new[] { "AuthorID" });
            DropIndex("dbo.ArticleComments", new[] { "ParentID" });
            DropIndex("dbo.ArticleComments", new[] { "UserID" });
            DropIndex("dbo.ArticleComments", new[] { "ArticleID" });
            DropTable("dbo.ArticleVisits");
            DropTable("dbo.ArticleTags");
            DropTable("dbo.ArticleCategories1");
            DropTable("dbo.ArticleCategories");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleComments");
        }
    }
}
