using DoAn_WebsiteBanQuanAoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_WebsiteBanQuanAoTreEm.Areas.Admin.Controllers
{
    public class ApiCategoryController : Controller
    {
        // GET: Admin/ApiCategory
        public ActionResult Index()
        {
            MyDbContext db = new MyDbContext();
            List<LoaiQuanAo> loai = db.loaiQuanAos.ToList();
            return View(loai);
        }
    }
}