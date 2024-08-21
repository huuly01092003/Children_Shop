using DoAn_WebsiteBanQuanAoTreEm.Filters;
using DoAn_WebsiteBanQuanAoTreEm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoAn_WebsiteBanQuanAoTreEm.Areas.Admin.Controllers
{
    [AdminAuthorization]

    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(string search = "", string SortColumn = "Name", int Page = 1)
        {
            MyDbContext db = new MyDbContext();

            // Lấy toàn bộ danh sách sản phẩm
            var qa = db.quanAos.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                qa = qa.Where(row => row.tenSP.Contains(search));
            }

            ViewBag.Search = search;

            // Sắp xếp
            switch (SortColumn)
            {
                case "NameAsc":
                    qa = qa.OrderBy(row => row.tenSP);
                    break;
                case "NameDesc":
                    qa = qa.OrderByDescending(row => row.tenSP);
                    break;
                case "GiaBanAsc":
                    qa = qa.OrderBy(row => row.giaBan);
                    break;
                case "GiaBanDesc":
                    qa = qa.OrderByDescending(row => row.giaBan);
                    break;
                default:
                    // Sắp xếp mặc định theo tên nếu không có sắp xếp nào được chọn
                    qa = qa.OrderBy(row => row.tenSP);
                    break;
            }

            // Phân trang
            int NoOfRecordPerPage = 8;
            int NoOfRecordToSkip = (Page - 1) * NoOfRecordPerPage;
            ViewBag.Page = Page;
            ViewBag.NoOfPage = (int)Math.Ceiling((double)qa.Count() / NoOfRecordPerPage);
            var items = qa.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            ViewBag.SortColumn = SortColumn;

            return View(items);
        }


        public ActionResult Create()
        {
            MyDbContext db = new MyDbContext();
            var loaiList = db.loaiQuanAos.ToList();

            if (loaiList != null)
            {
                ViewBag.Loai = loaiList;
            }
            else
            {
                ViewBag.Loai = new List<LoaiQuanAo>(); // Hoặc bạn có thể xử lý tùy thuộc vào logic của bạn.
            }
            var genList = db.quanAos.ToList();

            if (loaiList != null)
            {
                ViewBag.Gen = genList;
            }
            else
            {
                ViewBag.Gen = new List<QuanAo>(); // Hoặc bạn có thể xử lý tùy thuộc vào logic của bạn.
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(QuanAo product, HttpPostedFileBase imageFile)
        {
            MyDbContext db = new MyDbContext();
          
            if (ModelState.IsValid)
            {
              
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (imageFile.ContentLength > 2000000)
                    {
                        ModelState.AddModelError("img", "Kích thước file không được lớn hơn 2MB");
                        return View();
                    }
                    var allowExs = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();
                    if (!allowExs.Contains(fileEx))
                    {
                        ModelState.AddModelError("img", "Phần mở rộng file không hỗ trợ.");
                        return View();
                    }
                    product.img = "";
                    db.quanAos.Add(product);
                    db.SaveChanges();

                    QuanAo pro = db.quanAos.ToList().Last();

                    var fileName = pro.maQuanAo.ToString() + fileEx;
                    var path = Path.Combine(Server.MapPath("~/img/product"), fileName);
                    imageFile.SaveAs(path);

                    pro.img = fileName;
                    db.SaveChanges();
                    return RedirectToAction("Index","product","admin");
                }
                else
                {
                    product.img = "";
                    db.quanAos.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index","product","admin");
                }
            }
            else
            {
                return View();
            }

        }

       

        public ActionResult Delete(int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var itemdel = db.quanAos.FirstOrDefault(p => p.maQuanAo == id);
                if (itemdel != null)
                {
                    return View(itemdel);
                }
                else
                {
                    // Trả về một trang lỗi hoặc thực hiện xử lý khác khi item không tồn tại
                    return RedirectToAction("Index", "product", "admin");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,QuanAo p)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var pro = db.quanAos.FirstOrDefault(n => n.maQuanAo == id);
                if (pro != null)
                {
                    db.quanAos.Remove(pro);
                    int affectedRows = db.SaveChanges();
                    if (affectedRows > 0)
                    {
                        // Xóa thành công
                        return RedirectToAction("Index", "product", "admin");
                    }
                    else
                    {
                        // Xóa không thành công
                        // Có thể thực hiện xử lý khác tại đây, ví dụ, trả về một trang lỗi
                        return RedirectToAction("Error", "Home", new { area = "" });
                    }
                }
                else
                {
                    // Bản ghi không tồn tại
                    // Có thể thực hiện xử lý khác tại đây, ví dụ, trả về một trang lỗi
                    return RedirectToAction("Error", "Home", new { area = "" });
                }
            }
        }



                public ActionResult Edit(int id)
        {
            MyDbContext db = new MyDbContext();
            var loaiList = db.loaiQuanAos.ToList();

            if (loaiList != null)
            {
                ViewBag.Loai = loaiList;
            }
            else
            {
                ViewBag.Loai = new List<LoaiQuanAo>();
            }

            var genList = db.quanAos.ToList();

            if (loaiList != null)
            {
                var distinctGenders = db.quanAos.Select(q => q.gioiTinh).Distinct().ToList();
                ViewBag.Gen = distinctGenders;
            }
            else
            {
                ViewBag.Gen = new List<QuanAo>();
            }

            var item = db.quanAos.Where(n => n.maQuanAo == id).FirstOrDefault();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuanAo editedProduct, HttpPostedFileBase imageFile)
        {
            MyDbContext db = new MyDbContext();
            QuanAo p = db.quanAos.FirstOrDefault(x => x.maQuanAo == editedProduct.maQuanAo);

            if (p != null)
            {
                p.tenSP = editedProduct.tenSP;
                p.maLoai = editedProduct.maLoai;
                p.gioiTinh = editedProduct.gioiTinh;
                p.giaBan = editedProduct.giaBan;
                p.moTa = editedProduct.moTa;
                p.trangThai = editedProduct.trangThai;
                p.mau = editedProduct.mau;
                p.size = editedProduct.size;

                        if (imageFile != null && imageFile.ContentLength > 0)
                        {
                            int id = int.Parse(db.quanAos.ToList().Last().maQuanAo.ToString());
                            string filename = "";
                            int index = imageFile.FileName.IndexOf('.');
                            filename = "item" + id.ToString() + "." + imageFile.FileName.Substring(index + 1);
                            string path = Path.Combine(Server.MapPath("~/img/product"), filename);
                            imageFile.SaveAs(path);
                            p.img = filename;
                        }

                        db.SaveChanges();
                return RedirectToAction("Index", "Product", new { area = "admin" });
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy sản phẩm cần cập nhật.");
                return View(editedProduct);
            }
        }


    }
}