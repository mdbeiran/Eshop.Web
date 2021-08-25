namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductSelectedFeatures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductSelectedFeatures", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductSelectedFeatures", "IsDelete");
        }
    }
}
