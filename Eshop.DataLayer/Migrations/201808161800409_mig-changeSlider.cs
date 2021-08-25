namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangeSlider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(maxLength: 20),
                        Message = c.String(nullable: false, maxLength: 2000),
                        Answer = c.String(),
                        UserIP = c.String(maxLength: 20),
                        IsRead = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SiteSettings",
                c => new
                    {
                        SettingID = c.Int(nullable: false),
                        SiteDescription = c.String(nullable: false, maxLength: 500),
                        SiteEmail = c.String(nullable: false, maxLength: 300),
                        SiteTell = c.String(maxLength: 250),
                        SiteMobile = c.String(maxLength: 200),
                        Address = c.String(),
                        SiteFax = c.String(maxLength: 200),
                        SupportTell = c.String(maxLength: 200),
                        SupportEmail = c.String(maxLength: 250),
                        CopyRight = c.String(),
                    })
                .PrimaryKey(t => t.SettingID);
            
            AddColumn("dbo.Sliders", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Mobile", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactUs", "UserId", "dbo.Users");
            DropIndex("dbo.ContactUs", new[] { "UserId" });
            AlterColumn("dbo.Users", "Mobile", c => c.String(maxLength: 20));
            DropColumn("dbo.Sliders", "Text");
            DropTable("dbo.SiteSettings");
            DropTable("dbo.ContactUs");
        }
    }
}
