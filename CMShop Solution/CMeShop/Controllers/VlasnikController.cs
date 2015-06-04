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
    public class VlasnikController : Controller
    {
        private ShopContext db = new ShopContext();

        public ActionResult Details()
        {
            if (Session["id"] != null)
            {
                Vlasnik vlasnik = (Vlasnik)db.Korisnici.Find(Session["id"]);
                return View(vlasnik);
            }
            else
            {
                ViewBag.ErrorPoruka = "Niste logovani. Molimo vas da se prijavite kako bi ste mogli pristupiti vašem računu.";
                return View();
            }

        }

        public ActionResult Edit(int? id)
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            return View((Vlasnik)db.Korisnici.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Dostavljac dostavljac)
        {
            var userFromDb = (Vlasnik)db.Korisnici.Find(dostavljac.ID);
            userFromDb.ImeIprezime = dostavljac.ImeIprezime;
            userFromDb.password = dostavljac.password;
            userFromDb.userName = dostavljac.userName;
            userFromDb.adresa = dostavljac.adresa;
            userFromDb.brojTelefona = dostavljac.brojTelefona;
            userFromDb.email = dostavljac.email;
            db.Entry(userFromDb).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ViewBag.Poruka = "Uspjesno ste izmjenili korisničke podatke.";
            return View();
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
