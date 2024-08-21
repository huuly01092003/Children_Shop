using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DoAn_WebsiteBanQuanAoTreEm.Controllers;

namespace DoAn_WebsiteBanQuanAoTreEm.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext() : base("myConnect") { }
        public DbSet<QuanAo> quanAos { get; set; }
        //public DbSet<User> users { get; set; }
        public DbSet<LoaiQuanAo> loaiQuanAos { get; set; }
        
        public DbSet<GioHang> gioHangs { get; set;}

    }
}