namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migdeleteprandid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "BrandId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "BrandId", c => c.Int(nullable: false));
        }
    }
}
