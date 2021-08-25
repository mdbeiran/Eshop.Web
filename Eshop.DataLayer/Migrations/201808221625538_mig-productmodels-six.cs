namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migproductmodelssix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFeatures", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductFeatures", "IsDelete");
        }
    }
}
