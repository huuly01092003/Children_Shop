namespace DoAn_WebsiteBanQuanAoTreEm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropthanhtien : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GioHangs", "thanhTien");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GioHangs", "thanhTien", c => c.Int(nullable: false));
        }
    }
}
