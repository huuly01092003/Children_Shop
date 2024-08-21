using DoAn_WebsiteBanQuanAoTreEm.Identity;
using DoAn_WebsiteBanQuanAoTreEm.Models;
using DoAn_WebsiteBanQuanAoTreEm.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_WebsiteBanQuanAoTreEm.Filters;

namespace DoAn_WebsiteBanQuanAoTreEm.Controllers
{
    public class ProfileController : Controller
    {
        [MyAuThenFilter]

        // GET: Profile
        public ActionResult Index()
        {
            // Kiểm tra xem người dùng có đăng nhập hay không
            if (User.Identity.IsAuthenticated)
            {
                // Lấy thông tin người dùng từ UserManager
                var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new AppDbContext()));
                var user = userManager.FindByName(User.Identity.Name);

                // Kiểm tra xem user có tồn tại hay không
                if (user != null)
                {
                    // Lấy thông tin chi tiết của người dùng
                    string username = user.UserName;
                    string email = user.Email;
                    // Thêm thông tin khác nếu cần
                    string address=user.Address;
                    string phone = user.PhoneNumber;
                    ViewBag.Username = username;
                    ViewBag.Email = email;
                    ViewBag.Address = address;
                    ViewBag.Phone = phone;

                    // Thêm ViewBag cho các thông tin khác nếu cần
                }
            }
            else
            {
                // Người dùng chưa đăng nhập, xử lý theo ý của bạn
            }


            return View();
        }
    }
}