namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangeproductcomment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductComments", "Isdelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductComments", "Isdelete");
        }
    }
}
