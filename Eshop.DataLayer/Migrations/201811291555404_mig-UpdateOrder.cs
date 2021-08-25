namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migUpdateOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsSend", c => c.Boolean(nullable: false));
            AlterColumn("dbo.OrderDetails", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "Price", c => c.Long(nullable: false));
            AlterColumn("dbo.OrderDetails", "Count", c => c.Long(nullable: false));
            DropColumn("dbo.Orders", "IsSend");
        }
    }
}
