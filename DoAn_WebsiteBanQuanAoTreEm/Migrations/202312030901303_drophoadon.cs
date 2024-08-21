namespace DoAn_WebsiteBanQuanAoTreEm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drophoadon : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HoaDons", "userId", "dbo.Users");
            DropForeignKey("dbo.GioHangs", "maHD", "dbo.HoaDons");
            DropIndex("dbo.GioHangs", new[] { "maHD" });
            DropIndex("dbo.HoaDons", new[] { "userId" });
            DropColumn("dbo.GioHangs", "maHD");
            DropTable("dbo.HoaDons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        maHD = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        thueVAT = c.Single(nullable: false),
                        TongTien = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.maHD);
            
            AddColumn("dbo.GioHangs", "maHD", c => c.Int(nullable: false));
            CreateIndex("dbo.HoaDons", "userId");
            CreateIndex("dbo.GioHangs", "maHD");
            AddForeignKey("dbo.GioHangs", "maHD", "dbo.HoaDons", "maHD", cascadeDelete: true);
            AddForeignKey("dbo.HoaDons", "userId", "dbo.Users", "userId", cascadeDelete: true);
        }
    }
}
