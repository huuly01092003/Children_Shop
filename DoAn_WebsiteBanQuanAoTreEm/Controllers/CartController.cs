using DoAn_WebsiteBanQuanAoTreEm.Filters;
using DoAn_WebsiteBanQuanAoTreEm.Identity;
using DoAn_WebsiteBanQuanAoTreEm.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_WebsiteBanQuanAoTreEm.Controllers
{
    [MyAuThenFilter]

    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Lấy Id của người dùng hiện tại
                string userId = User.Identity.GetUserId(); // Đối với Identity Framework

                // Tạo một đối tượng của DbContext
                MyDbContext db = new MyDbContext();

                // Lấy giỏ hàng của người dùng hiện tại
                List<GioHang> carts = db.gioHangs.Where(g => g.UserId == userId).ToList();

                // Truyền danh sách giỏ hàng đến View
                return View(carts);
            }
            else
            {
                // Người dùng chưa đăng nhập, xử lý theo ý của bạn (ví dụ: chuyển hướng đến trang đăng nhập)
                return RedirectToAction("Login", "Account");
            }
        }

        //public ActionResult Add(int id = 0)
        //{
        //    if (id > 0)
        //    {
        //        MyDbContext db = new MyDbContext();
        //        GioHang cartItem = db.gioHangs.Where(c => c.maQuanAo == id).FirstOrDefault();
        //        if (cartItem != null)
        //        {
        //            cartItem.soLuong += 1;
        //        }
        //        else
        //        {
        //            GioHang cart = new GioHang();
        //            cart.maQuanAo = id;
        //            cart.soLuong = 1;
        //            db.gioHangs.Add(cart);
        //        }
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("Index");
        //}

        //public ActionResult UpdateQuantity(int quan = 0, int proid = 0)
        //{
        //    MyDbContext db = new MyDbContext();
        //    if (quan > 0)
        //    {
        //        GioHang cartItem = db.gioHangs.Where(c => c.maQuanAo == proid).FirstOrDefault();
        //        if (cartItem != null)
        //        {
        //            cartItem.soLuong = quan;
        //            db.SaveChanges();
        //        }
        //    }

        //    return RedirectToAction("Index");
        //}
        //public ActionResult Remove(int id = 0)
        //{
        //    if (id > 0)
        //    {
        //        MyDbContext db = new MyDbContext();
        //        GioHang cartItem = db.gioHangs.Where(c => c.maQuanAo == id).FirstOrDefault();
        //        if (cartItem != null)
        //        {
        //            db.gioHangs.Remove(cartItem);
        //            db.SaveChanges();
        //        }
        //    }

        //    return RedirectToAction("Index");
        //}

        public ActionResult Add(int id = 0)
        {
            if (id > 0)
            {
                MyDbContext db = new MyDbContext();
                string userId = User.Identity.GetUserId(); // Adjust this based on your authentication setup

                GioHang cartItem = db.gioHangs.Where(c => c.maQuanAo == id && c.UserId == userId).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.soLuong += 1;
                }
                else
                {
                    GioHang cart = new GioHang();
                    cart.maQuanAo = id;
                    cart.soLuong = 1;
                    cart.UserId = userId;
                    db.gioHangs.Add(cart);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult UpdateQuantity(int quan = 0, int proid = 0)
        {
            MyDbContext db = new MyDbContext();
            string userId = User.Identity.GetUserId(); // Adjust this based on your authentication setup

            if (quan > 0)
            {
                GioHang cartItem = db.gioHangs.Where(c => c.maQuanAo == proid && c.UserId == userId).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.soLuong = quan;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id = 0)
        {
            if (id > 0)
            {
                MyDbContext db = new MyDbContext();
                string userId = User.Identity.GetUserId(); // Adjust this based on your authentication setup

                GioHang cartItem = db.gioHangs.Where(c => c.maQuanAo == id && c.UserId == userId).FirstOrDefault();
                if (cartItem != null)
                {
                    db.gioHangs.Remove(cartItem);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}