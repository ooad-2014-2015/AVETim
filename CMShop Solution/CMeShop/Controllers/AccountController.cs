using CMeShop.DAL;
using CMeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading;

namespace CMeShop.Controllers
{
    public class AccountController : Controller
    {
        private ShopContext db = new ShopContext();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Kupac kupac)
        {
            if (ModelState.IsValid)
            {
                var count = db.Korisnici.Where(x => x.userName == kupac.userName).Count();
                if (count > 0) { ViewBag.Poruka = "Već postoji registrovan kupac sa datim korisničkim imenom. Izaberite drugi username."; return View(); }
                kupac.Kosarica = new Kosarica();
                db.Korisnici.Add(kupac);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CMeShop.ViewModels.LoginViewModel user)
        {
            var count = db.Korisnici.Where(x => x.userName == user.userName && x.password == user.password).Count();
            if (count == 0)
            {
                ViewBag.Poruka = "Podaci za prijavu su netačni. Molimo Vas provjerite vaše korisničko ime i lozinku.";
                return View();
            }
            else
            {
                var userFromDb = db.Korisnici.Where(x => x.userName == user.userName).First();
                Session["username"] = userFromDb.userName;
                Session["id"] = userFromDb.ID;
                Session["role"] = userFromDb.role;
                if (userFromDb.role == "Kupac") Session["StavkeKosarice"] = new List<CMeShop.Models.StavkaKosarice>();
                return RedirectToAction("Index", "Home");
            }
        }
/*
        public ActionResult Login()
        {
            if (Session["username"] != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kupac kupac)
        {
            var count = db.Korisnici.Where(x => x.userName == kupac.userName && x.password == kupac.password).Count();
            if (count == 0)
            {
                ViewBag.Poruka = "Podaci za prijavu su netačni. Molimo Vas provjerite vaše korisničko ime i lozinku.";
                return View();
            }
            else
            {
                var userFromDb = db.Korisnici.Where(x => x.userName == kupac.userName).First();
                if (userFromDb.role == "Vlasnik") { ViewBag.ErrorModel = "Vlasnik" ; return View(); }
                else if (userFromDb.role == "Dostavljac") { ViewBag.ErrorModel = "Ovdje se mogu prijaviti samo registrovani kupci. Ukoliko se zelite prijaviti kao Dostavljač to možete učiniti "; return View(); }
                else if (userFromDb.role == "Dobavljac") { ViewBag.ErrorModel = "Ovdje se mogu prijaviti samo registrovani kupci. Ukoliko se zelite prijaviti kao Dobavljač to možete učiniti "; return View(); }
                FormsAuthentication.SetAuthCookie(kupac.userName, false);
                Session["username"] = userFromDb.userName;
                Session["id"] = userFromDb.ID;
                Session["role"] = userFromDb.role;
                Session["StavkeKosarice"] = new List<StavkaKosarice>();
                return RedirectToAction("Index", "Home");
            }
        }*/
        public ActionResult Details()
        {
            Kupac kupac = null;
            Thread mojThread = new Thread(() => prikupiDetalje(out kupac));
            mojThread.Start();
            while (!mojThread.IsAlive) ;
            mojThread.Join();
            if (kupac != null) return View(kupac);
            return View();
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        public void prikupiDetalje(out Kupac kupac)
        {
            if (Session["id"] != null)
            {
                kupac = (Kupac)db.Korisnici.Find(Session["id"]);
            }
            else
            {
                kupac = null;
                ViewBag.ErrorPoruka = "Niste logovani. Molimo vas da se prijavite kako bi ste mogli pristupiti vašem računu.";
            }
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