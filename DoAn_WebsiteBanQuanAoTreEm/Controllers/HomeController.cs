using DoAn_WebsiteBanQuanAoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebsiteBanQuanAoTreEm.Filters;
namespace DoAn_WebsiteBanQuanAoTreEm.Controllers
{
    public class HomeController : Controller
    {
        // GET: Product
      
        public ActionResult Index(string search = "", string SortColumn = "Name", int Page = 1, int? LoaiQuanAo = null)
        {
            MyDbContext db = new MyDbContext();
            List<QuanAo> qa;

            //Search
            if (!string.IsNullOrEmpty(search))
            {
                qa = db.quanAos.Where(row => row.tenSP.Contains(search)).ToList();
            }
            else
            {
                qa = db.quanAos.ToList();
            }

            ViewBag.Search = search;


            //Paging
            int NoOfRecordPerPage = 8;
            int NoOfPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(qa.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (Page - 1) * NoOfRecordPerPage;
            ViewBag.Page = Page;
            ViewBag.NoOfPage = NoOfPage;
            qa = qa.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();



            //Sort

            if (SortColumn.StartsWith("NameAsc"))
            {
                qa = qa.OrderBy(row => row.tenSP).ToList();

            }
            else if (SortColumn.StartsWith("NameDesc"))
            {
                qa = qa.OrderByDescending(row => row.tenSP).ToList();

            }
          
            else if (SortColumn.StartsWith("GiaBanAsc"))
            {
                qa = qa.OrderBy(row => row.giaBan).ToList();

            }
            else if (SortColumn.StartsWith("GiaBanDesc"))
            {
                qa = qa.OrderByDescending(row => row.giaBan).ToList();
            }
    
            ViewBag.SortColumn = SortColumn;

            //List<LoaiQuanAo> loaiSanPhams = db.loaiQuanAos.ToList();
            //if (loaiSanPhams..HasValue)
            //{
            //    quanAos = db.QuanAos.Where(q => q.maLoai == maLoai.Value).ToList();
            //}
            //else
            //{
            //    quanAos = db.QuanAos.ToList();
            //}


            if (LoaiQuanAo.HasValue)
            {
                qa = qa.Where(row => row.maLoai == LoaiQuanAo).ToList();
            }
            ViewBag.LoaiQuanAos = db.loaiQuanAos.ToList(); // Load danh sách loại quần áo để hiển thị trong dropdown

            return View(qa);
        }
        [MyAuThenFilter]
        public ActionResult Detail(int? id)
        {
            MyDbContext db = new MyDbContext();
            QuanAo qa = db.quanAos.Where(row => row.maQuanAo == id).FirstOrDefault();
            return View(qa);
        }
        public ActionResult Add()
        {
            var roleCookie = Request.Cookies["role"];
            if (roleCookie != null)
            {
                if (roleCookie.Value == "admin")
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "User");
        }
        
    }
}