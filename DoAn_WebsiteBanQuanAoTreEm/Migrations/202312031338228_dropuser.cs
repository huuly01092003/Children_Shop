namespace DoAn_WebsiteBanQuanAoTreEm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropuser : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        password = c.String(nullable: false),
                        email = c.String(nullable: false),
                        role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.userId);
            
        }
    }
}
