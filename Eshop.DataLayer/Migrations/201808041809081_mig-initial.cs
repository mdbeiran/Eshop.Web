namespace Eshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miginitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Name = c.String(nullable: false, maxLength: 250),
                        ParentId = c.Int(),
                        DisplayPriority = c.Int(),
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.Permissions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.RolePersmissions",
                c => new
                    {
                        RolePermissionId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RolePermissionId)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleTitle = c.String(nullable: false, maxLength: 100),
                        IsDefaultRole = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserSelectedRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        Mobile = c.String(maxLength: 20),
                        Password = c.String(maxLength: 50),
                        MobileActiveCode = c.Int(nullable: false),
                        RegisterIP = c.String(maxLength: 20),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ViewByAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "ParentId", "dbo.Permissions");
            DropForeignKey("dbo.RolePersmissions", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.UserSelectedRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSelectedRoles", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.RolePersmissions", "RoleId", "dbo.UserRoles");
            DropIndex("dbo.UserSelectedRoles", new[] { "RoleId" });
            DropIndex("dbo.UserSelectedRoles", new[] { "UserId" });
            DropIndex("dbo.RolePersmissions", new[] { "PermissionId" });
            DropIndex("dbo.RolePersmissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "ParentId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserSelectedRoles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.RolePersmissions");
            DropTable("dbo.Permissions");
        }
    }
}
