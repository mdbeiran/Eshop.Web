namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migchangeSlidersecond : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sliders", "Text", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sliders", "Text", c => c.String(nullable: false));
        }
    }
}
