namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangesitesetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSettings", "SiteName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSettings", "SiteName");
        }
    }
}
