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

        [HttpPost]
        public ActionResult Index(List<StavkaKosarice> updateovaneStavke)
        {
            foreach(var item in updateovaneStavke)
            {
                if(item.isporuceno == true) //doslo je do izmjene
                {
                    var stavka = db.StavkeKosarice.Find(item.ID);
                    stavka.isporuceno = true;
                    db.Entry(stavka).State = EntityState.Modified;
                    var kosarica = db.Kosarice.Find(item.KosaricaID);
                    db.Entry(kosarica).State = EntityState.Modified;
                    var artikal = db.Artikli.Find(item.ArtikalID);
                    db.Entry(artikal).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Dostavljac");
        }
        public ActionResult Index()
        {
            if ((string)Session["role"] != "Dostavljac") return View("~/Views/Shared/Error.cshtml");
            var stavke = db.StavkeKosarice.Include(a => a.artikal).Where(b => b.isporuceno==false);
            return View(stavke.ToList());
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
            if ((string)Session["role"] != "Dostavljac") return View("~/Views/Shared/Error.cshtml");
            if (Session["id"] != null)
            {
                Dostavljac dostavljac = (Dostavljac)db.Korisnici.Find(Session["id"]);
                return View(dostavljac);
            }
            else
            {
                ViewBag.ErrorPoruka = "Niste logovani. Molimo vas da se prijavite kako bi ste mogli pristupiti vašem računu.";
                return View();
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
