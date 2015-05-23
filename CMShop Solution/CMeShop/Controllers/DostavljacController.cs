using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMeShop.DAL;
using CMeShop.Models;
using System.Web.Security;

namespace CMeShop.Controllers
{
    public class DostavljacController : Controller
    {
        private ShopContext db = new ShopContext();


        public ActionResult Login()
        {
            if (Session["username"] != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Dostavljac dostavljac)
        {
            var count = db.Korisnici.Where(x => x.userName == dostavljac.userName && x.password == dostavljac.password).Count();
            if (count == 0)
            {
                ViewBag.Poruka = "Podaci za prijavu su netačni. Molimo Vas provjerite vaše korisničko ime i lozinku.";
                return View();
            }
            else
            {
                var userFromDb = db.Korisnici.Where(x => x.userName == dostavljac.userName).First();
                if (userFromDb.role != "Dostavljac") { ViewBag.ErrorModel = "NotDostavljac"; return View(); }
                FormsAuthentication.SetAuthCookie(dostavljac.userName, false);
                Session["username"] = userFromDb.userName;
                Session["id"] = userFromDb.ID;
                Session["role"] = userFromDb.role;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Details()
        {
            if (Session["id"] != null)
            {
                Dostavljac vlasnik = (Dostavljac)db.Korisnici.Find(Session["id"]);
                return View(vlasnik);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
