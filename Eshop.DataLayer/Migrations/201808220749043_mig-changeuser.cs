namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangeuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Address", c => c.String(maxLength: 500));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 100));
            DropColumn("dbo.Users", "RegisterIP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RegisterIP", c => c.String(maxLength: 20));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "UserName", c => c.String(maxLength: 100));
            DropColumn("dbo.Users", "Address");
        }
    }
}
