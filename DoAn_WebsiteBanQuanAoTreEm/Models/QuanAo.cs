using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAn_WebsiteBanQuanAoTreEm.Models
{
    public class QuanAo
    {
        [Key]
        public int maQuanAo { get; set; }
        public int maLoai { get; set; }
        public string tenSP { get; set; }
        public string gioiTinh { get; set; }
        public int  giaBan { get; set; }
        public string moTa { get; set; }
        public string trangThai { get; set; }
        public string size { get; set; }
        public string mau { get; set; }
        public string img { get; set; }
        public virtual LoaiQuanAo loai { get; set; }
        public virtual ICollection<GioHang> gioHang { get; set; }

    }
}