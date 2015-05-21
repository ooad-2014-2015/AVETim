using CMeShop.DAL;
using CMeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMeShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Kupac kupac)
        {
            ShopContext db = new ShopContext();
            var count = db.Korisnici.Where(x => x.userName == kupac.userName && x.password == kupac.password).Count();
            if(count == 0)
            {
                ViewBag.Poruka = "Nepostojeći korisnik";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(kupac.userName, false);
                return RedirectToAction("Index", "Home");
            }

        }
    }
}