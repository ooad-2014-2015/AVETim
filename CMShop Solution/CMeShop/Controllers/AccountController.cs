using CMeShop.DAL;
using CMeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading;
using System.IO;
using System.Web.UI.WebControls;

namespace CMeShop.Controllers
{
    public class AccountController : Controller
    {
        private ShopContext db = new ShopContext();
        public ActionResult Register()
        {
            if (!(Session["id"] == null)) return View("~/Views/Shared/Error.cshtml");
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
                kupac.role = "Kupac";
                db.Korisnici.Add(kupac);
                db.SaveChanges();
                LogirajKorisnika(kupac.userName);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Login()
        {
            if (!(Session["id"] == null)) return View("~/Views/Shared/Error.cshtml");
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
                LogirajKorisnika(user.userName);
                return RedirectToAction("Index", "Home");
            }
        }

        private void LogirajKorisnika(string userName)
        {
            var userFromDb = db.Korisnici.Where(x => x.userName == userName).First();
            Session["username"] = userFromDb.userName;
            Session["id"] = userFromDb.ID;
            Session["role"] = userFromDb.role;
            if (userFromDb.role == "Kupac") Session["StavkeKosarice"] = new List<CMeShop.Models.StavkaKosarice>();
        }
        public ActionResult Edit(int? id)
        {
            if (Session["id"] == null || (string)Session["role"] != "Kupac" || id != (int)Session["id"])
                ViewBag.Poruka = "Možete izmjeniti podatke, samo kada ste prijavljeni, i to od vlastitog računa.";
            return View((Kupac)db.Korisnici.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Kupac kupac)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = (Kupac)db.Korisnici.Find(kupac.ID);
                userFromDb.ImeIprezime = kupac.ImeIprezime;
                userFromDb.password = kupac.password;
                userFromDb.userName = kupac.userName;
                userFromDb.brojCMkartice = kupac.brojCMkartice;
                userFromDb.bankovniRacun = kupac.bankovniRacun;
                userFromDb.adresa = kupac.adresa;
                userFromDb.brojTelefona = kupac.brojTelefona;
                var kosarica = db.Kosarice.Find(kupac.KosaricaID);
                db.Entry(kosarica).State = System.Data.Entity.EntityState.Modified;
                db.Entry(userFromDb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Poruka = "Uspjesno ste izmjenili korisničke podatke.";
            }
            return View();
        }
        /*Koristen thread*/
        public ActionResult Details()
        {
            if((string)Session["role"] != "Kupac") return View("~/Views/Shared/Error.cshtml");
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