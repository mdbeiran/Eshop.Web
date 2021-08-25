namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangeproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SellCount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SellCount");
        }
    }
}
