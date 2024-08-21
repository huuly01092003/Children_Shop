namespace DoAn_WebsiteBanQuanAoTreEm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GioHangs", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GioHangs", "UserId");
        }
    }
}
