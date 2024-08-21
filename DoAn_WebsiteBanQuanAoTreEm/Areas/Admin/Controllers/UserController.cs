using DoAn_WebsiteBanQuanAoTreEm.Filters;
using DoAn_WebsiteBanQuanAoTreEm.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_WebsiteBanQuanAoTreEm.Areas.Admin.Controllers
{
    [AdminAuthorization]

    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            AppDbContext db = new AppDbContext();
            List<AppUser> users = db.Users.ToList();
            return View(users);
        }

        public ActionResult Delete(string id)
        {
            AppDbContext db = new AppDbContext();
            var itemdel = db.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(itemdel);
        }

        [HttpPost]
        public ActionResult Delete(AppUser u)
        {
            AppDbContext db = new AppDbContext();
            var user = db.Users.Where(n => n.Id == u.Id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AppUser usr)
        {
            AppDbContext db = new AppDbContext();
            db.Users.Add(usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            AppDbContext db = new AppDbContext();
            var user = db.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(AppUser editedUser)
        {
            AppDbContext db = new AppDbContext();
            AppUser user = db.Users.FirstOrDefault(x => x.Id == editedUser.Id);

            if (user != null)
            {
                // Cập nhật thông tin người dùng từ editedUser
                user.UserName = editedUser.UserName;
                user.Email = editedUser.Email;
                user.PhoneNumber = editedUser.PhoneNumber;
                user.Birthday = editedUser.Birthday;
                user.Address = editedUser.Address;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}