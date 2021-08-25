namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migproductmodelsfifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductGroups", "NameInUrl", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductGroups", "NameInUrl");
        }
    }
}
