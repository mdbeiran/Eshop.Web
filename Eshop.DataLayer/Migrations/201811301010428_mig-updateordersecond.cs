namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migupdateordersecond : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "StatusCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "StatusCode");
        }
    }
}
