namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migproductmodelseight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsActive");
            DropColumn("dbo.Products", "IsDelete");
        }
    }
}
