namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangecontactusthird : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SiteSettings", "SiteDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SiteSettings", "SiteDescription", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
