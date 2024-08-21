namespace DoAn_WebsiteBanQuanAoTreEm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GioHangs",
                c => new
                    {
                        maGioHang = c.Int(nullable: false, identity: true),
                        maHD = c.Int(nullable: false),
                        maQuanAo = c.Int(nullable: false),
                        soLuong = c.Int(nullable: false),
                        thanhTien = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.maGioHang)
                .ForeignKey("dbo.HoaDons", t => t.maHD, cascadeDelete: true)
                .ForeignKey("dbo.QuanAos", t => t.maQuanAo, cascadeDelete: true)
                .Index(t => t.maHD)
                .Index(t => t.maQuanAo);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        maHD = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        thueVAT = c.Single(nullable: false),
                        TongTien = c.Int(nullable: false),
                        Users_userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.maHD)
                .ForeignKey("dbo.Users", t => t.Users_userId)
                .Index(t => t.Users_userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        name = c.String(nullable: false),
                        password = c.String(nullable: false),
                        email = c.String(nullable: false),
                        role = c.String(nullable: false,defaultValue:"user"),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.QuanAos",
                c => new
                    {
                        maQuanAo = c.Int(nullable: false, identity: true),
                        maLoai = c.Int(nullable: false),
                        tenSP = c.String(),
                        gioiTinh = c.String(),
                        giaBan = c.Int(nullable: false),
                        moTa = c.String(),
                        trangThai = c.String(),
                        size = c.String(),
                        mau = c.String(),
                        img = c.String(),
                    })
                .PrimaryKey(t => t.maQuanAo)
                .ForeignKey("dbo.LoaiQuanAos", t => t.maLoai, cascadeDelete: true)
                .Index(t => t.maLoai);
            
            CreateTable(
                "dbo.LoaiQuanAos",
                c => new
                    {
                        maLoai = c.Int(nullable: false, identity: true),
                        tenLoai = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.maLoai);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GioHangs", "maQuanAo", "dbo.QuanAos");
            DropForeignKey("dbo.QuanAos", "maLoai", "dbo.LoaiQuanAos");
            DropForeignKey("dbo.GioHangs", "maHD", "dbo.HoaDons");
            DropForeignKey("dbo.HoaDons", "Users_userId", "dbo.Users");
            DropIndex("dbo.QuanAos", new[] { "maLoai" });
            DropIndex("dbo.HoaDons", new[] { "Users_userId" });
            DropIndex("dbo.GioHangs", new[] { "maQuanAo" });
            DropIndex("dbo.GioHangs", new[] { "maHD" });
            DropTable("dbo.LoaiQuanAos");
            DropTable("dbo.QuanAos");
            DropTable("dbo.Users");
            DropTable("dbo.HoaDons");
            DropTable("dbo.GioHangs");
        }
    }
}
