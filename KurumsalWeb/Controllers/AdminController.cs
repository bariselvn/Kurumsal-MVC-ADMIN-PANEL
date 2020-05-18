using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        KurumsalDBEntities db = new KurumsalDBEntities();
        public ActionResult Index()
        {
            var query = db.Category.ToList();
            return View(query);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            
            var login = db.Admin.Where(x => x.Mail == admin.Mail).SingleOrDefault();
            if (login != null) {
                if (login.Mail == admin.Mail && login.password == admin.password)
                {
                    Session["adminId"] = login.AdminId;
                    Session["mail"] = login.Mail;

                    return RedirectToAction("Index", "Admin");
                }
             
                
            }
            ViewBag.Uyari = ("Email or Password is Wrong");
            return View();

        }
        public ActionResult Logout()
        {
            Session["adminId"] = null;
            Session["mail"] = null;
            Session.Abandon();

            return RedirectToAction("Login", "Admin");
        }
    }
}