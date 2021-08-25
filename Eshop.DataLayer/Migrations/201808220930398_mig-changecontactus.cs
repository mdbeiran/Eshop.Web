namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangecontactus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactUs", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactUs", "Email", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
