using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAn_WebsiteBanQuanAoTreEm.Models
{
    public class GioHang
    {
        [Key]
        public int maGioHang { get; set; }
        public int maQuanAo { get; set; }
        public int soLuong { get; set;}
        public string UserId { get; set; }
        public virtual QuanAo QuanAos { get; set; }
       
    }
}