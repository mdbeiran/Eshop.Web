namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangecontactussecond : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactUs", "Mobile", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactUs", "Mobile", c => c.String(maxLength: 20));
        }
    }
}
