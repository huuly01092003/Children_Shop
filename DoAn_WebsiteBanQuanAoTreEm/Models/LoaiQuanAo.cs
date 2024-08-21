using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAn_WebsiteBanQuanAoTreEm.Models
{
    public class LoaiQuanAo
    {
        [Key]
        public int maLoai { get; set; }
        [Required]
        public string tenLoai { get; set; }
        public virtual ICollection<QuanAo> quanAo { get; set; }
    }
}