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
            if (Session["username"] != null) return RedirectToAction("Index", "Home");
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
                Session["username"] = kupac.userName;
                Session["id"] = db.Korisnici.Where(x => x.userName == kupac.userName).First().ID;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Details()
        {
            if(Session["id"] != null)
            {
                ShopContext db = new ShopContext();
                Kupac kupac = (Kupac)db.Korisnici.Find(Session["id"]);
                return View(kupac);
            }
            else
            {
                ViewBag.ErrorPoruka = "Niste logovani. Molimo vas da se prijavite kako bi ste mogli pristupiti vašem računu.";
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}