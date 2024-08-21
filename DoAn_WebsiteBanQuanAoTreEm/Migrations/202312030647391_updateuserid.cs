namespace DoAn_WebsiteBanQuanAoTreEm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuserid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HoaDons", "Users_userId", "dbo.Users");
            DropIndex("dbo.HoaDons", new[] { "Users_userId" });
            DropColumn("dbo.HoaDons", "userId");
            RenameColumn(table: "dbo.HoaDons", name: "Users_userId", newName: "userId");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.HoaDons", "userId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "userId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "userId");
            CreateIndex("dbo.HoaDons", "userId");
            AddForeignKey("dbo.HoaDons", "userId", "dbo.Users", "userId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HoaDons", "userId", "dbo.Users");
            DropIndex("dbo.HoaDons", new[] { "userId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "userId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.HoaDons", "userId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Users", "userId");
            RenameColumn(table: "dbo.HoaDons", name: "userId", newName: "Users_userId");
            AddColumn("dbo.HoaDons", "userId", c => c.Int(nullable: false));
            CreateIndex("dbo.HoaDons", "Users_userId");
            AddForeignKey("dbo.HoaDons", "Users_userId", "dbo.Users", "userId");
        }
    }
}
