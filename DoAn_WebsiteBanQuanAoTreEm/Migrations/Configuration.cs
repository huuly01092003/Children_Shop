namespace DoAn_WebsiteBanQuanAoTreEm.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DoAn_WebsiteBanQuanAoTreEm.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DoAn_WebsiteBanQuanAoTreEm.Models.MyDbContext";
        }

        protected override void Seed(DoAn_WebsiteBanQuanAoTreEm.Models.MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
