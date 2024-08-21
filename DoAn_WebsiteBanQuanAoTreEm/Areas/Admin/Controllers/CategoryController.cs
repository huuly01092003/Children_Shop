using DoAn_WebsiteBanQuanAoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebsiteBanQuanAoTreEm.Filters;
namespace DoAn_WebsiteBanQuanAoTreEm.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            MyDbContext db = new MyDbContext();
            List<LoaiQuanAo> loai = db.loaiQuanAos.ToList();
            return View(loai);
        }
        public ActionResult Insert()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Insert(LoaiQuanAo loai)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu đã nhập từ biểu mẫu có hợp lệ không
            {
                MyDbContext db = new MyDbContext();
                try
                {
                    db.loaiQuanAos.Add(loai);
                    db.SaveChanges();
                    return RedirectToAction("index","category","admin");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi thêm sản phẩm: " + ex.Message);
                }
            }

            // Nếu có lỗi, quay lại trang thêm sản phẩm và hiển thị thông báo lỗi
            return View(loai);
        }
        public ActionResult Delete(int id)
        {
            MyDbContext db = new MyDbContext();
            LoaiQuanAo loai = db.loaiQuanAos.Where(row => row.maLoai == id).FirstOrDefault();

            return View(loai);
        }
        [HttpPost]
        public ActionResult Delete(int id, LoaiQuanAo ds)
        {
            MyDbContext db = new MyDbContext();
            LoaiQuanAo loai = db.loaiQuanAos.Where(row => row.maLoai == id).FirstOrDefault();
            db.loaiQuanAos.Remove(loai);
            db.SaveChanges();
            return RedirectToAction("Index","category","admin");
        }
        public ActionResult Update(int id)
        {
            MyDbContext db = new MyDbContext();
            LoaiQuanAo loai = db.loaiQuanAos.Find(id);

            if (loai == null)
            {
                // Handle the case where the record with the given id is not found
                return HttpNotFound();
            }

            return View(loai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LoaiQuanAo ds)
        {
            MyDbContext db = new MyDbContext();
            LoaiQuanAo loai = db.loaiQuanAos.Find(ds.maLoai);

            if (loai != null)
            {
                loai.tenLoai = ds.tenLoai;
                db.SaveChanges();
                return RedirectToAction("Index", "category", new { area = "admin" });
            }
            else
            {
                // Handle the case where the record with the given id is not found
                ModelState.AddModelError("", "Không tìm thấy loại quần áo cần cập nhật.");
                return View(ds);
            }
        }


    }
}