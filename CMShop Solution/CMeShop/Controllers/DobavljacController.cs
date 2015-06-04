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
using CMeShop.ViewModels;

namespace CMeShop.Controllers
{
    public class DobavljacController : Controller
    {
        private ShopContext db = new ShopContext();

        const int minimalnoStanje = 10;

        [HttpPost]
        public ActionResult Index(List<DobavljacArtikalViewModel> updateovaniArtikli)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in updateovaniArtikli)
                {
                    if (item.updateovano == true)
                    {
                        var artikal = db.Artikli.Find(item.ID);
                        artikal.zaliheStanje += item.kolicina;
                        db.Entry(artikal).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Dobavljac");
        }

        public ActionResult Index()
        {
            if ((string)Session["role"] != "Dobavljac") return View("~/Views/Shared/Error.cshtml");
            List<DobavljacArtikalViewModel> artikli = new List<DobavljacArtikalViewModel>();
            foreach (var item in db.Artikli)
            {
                if (item.zaliheStanje <= minimalnoStanje) artikli.Add(new DobavljacArtikalViewModel
                {
                    ID = item.ID,
                    cijena = item.cijena,
                    kolicina = 0,
                    naziv = item.naziv,
                    opis = item.opis,
                    proizvodjac = item.proizvodjac,
                    updateovano = false,
                    zaliheStanje = item.zaliheStanje,
                    zemljaPorijekla = item.zemljaPorijekla
                });
            }
            return View(artikli);
        }

        public ActionResult Details(int? id)
        {
            if (!((string)Session["role"] == "Dobavljac" && id.Value == (int)Session["id"]) && (string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            if (Session["id"] != null && (string)Session["role"] == "Dobavljac")
            {
                Dobavljac dobavljac = (Dobavljac)db.Korisnici.Find(Session["id"]);
                return View(dobavljac);
            }
            else if (id != null && (string)Session["role"] == "Vlasnik")
            {
                Dobavljac dobavljac = (Dobavljac)db.Korisnici.Find(id);
                return View(dobavljac);
            }
            else
            {
                ViewBag.ErrorPoruka = "Niste logovani. Molimo vas da se prijavite kako bi ste mogli pristupiti vašem računu.";
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dobavljac user = (Dobavljac)db.Korisnici.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dobavljac user = (Dobavljac)db.Korisnici.Find(id);
            db.Korisnici.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (!((string)Session["role"] == "Dobavljac" && id.Value == (int)Session["id"]) && (string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            ViewBag.Poruka = "Možete izmjeniti podatke, samo kada ste prijavljeni, i to od vlastitog računa, ili ako ste Vlasnik.";
            return View((Dobavljac)db.Korisnici.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Dobavljac dobavljac)
        {
            var userFromDb = (Dobavljac)db.Korisnici.Find(dobavljac.ID);
            userFromDb.ImeIprezime = dobavljac.ImeIprezime;
            userFromDb.password = dobavljac.password;
            userFromDb.userName = dobavljac.userName;
            userFromDb.adresa = dobavljac.adresa;
            userFromDb.brojTelefona = dobavljac.brojTelefona;
            userFromDb.email = dobavljac.email;
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
