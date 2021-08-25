namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migcreateSiteRules : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteSettings", "SiteRules", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteSettings", "SiteRules");
        }
    }
}
