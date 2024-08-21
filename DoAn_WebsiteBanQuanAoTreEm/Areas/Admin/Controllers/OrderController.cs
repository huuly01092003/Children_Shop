using DoAn_WebsiteBanQuanAoTreEm.Filters;
using DoAn_WebsiteBanQuanAoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DoAn_WebsiteBanQuanAoTreEm.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index()
        {
            MyDbContext db = new MyDbContext();
            List<GioHang> cart = db.gioHangs.ToList();
            return View(cart);
        }
        public ActionResult Remove(int cartItemId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                // Tìm item trong giỏ hàng theo ID
                GioHang itemToRemove = db.gioHangs.Find(cartItemId);

                if (itemToRemove != null)
                {
                    // Xóa item từ database
                    db.gioHangs.Remove(itemToRemove);
                    db.SaveChanges();
                }

                // Redirect hoặc trả về view tùy thuộc vào yêu cầu của bạn
                return RedirectToAction("Index","Order","Admin");
            }
        }
    }
 
}