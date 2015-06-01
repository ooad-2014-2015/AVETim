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

namespace CMeShop.Controllers
{
    public class KorisniciController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Korisnici
        public ActionResult Index()
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            return View();
        }

        public ActionResult Kupci()
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            List<Kupac> kupci = new List<Kupac>();
            foreach (var item in db.Korisnici.ToList())
	        {
		        Kupac kupac = item as Kupac;
                if(kupac != null && kupac.role == "Kupac")
                    kupci.Add(kupac);
	        }
            return View(kupci);
        }

        public ActionResult Dostavljaci()
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            List<Dostavljac> dostavljaci = new List<Dostavljac>();
            foreach (var item in db.Korisnici.ToList())
            {
                Dostavljac dost = item as Dostavljac;
                if (dost != null && dost.role == "Dostavljac")
                    dostavljaci.Add(dost);
            }
            return View(dostavljaci);
        }

        public ActionResult Dobavljaci()
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            List<Dobavljac> dobavljaci = new List<Dobavljac>();
            foreach (var item in db.Korisnici.ToList())
            {
                Dobavljac dob = item as Dobavljac;
                if (dob != null && dob.role == "Dobavljac")
                    dobavljaci.Add(dob);
            }
            return View(dobavljaci);
        }

        // GET: Korisnici/Details/5
        public ActionResult Details(int? id)
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Korisnici.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // GET: Korisnici/Create
        public ActionResult Create()
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            return View();
        }

        // POST: Korisnici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImeIprezime,brojTelefona,adresa,userName,password,slika")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                if(osoba.role == "Kupac")
                {
                    var kupac = osoba as Kupac;
                    kupac.Kosarica = new Kosarica();
                    db.Korisnici.Add(kupac);
                    db.SaveChanges();
                }
                else
                {
                    db.Korisnici.Add(osoba);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(osoba);
        }

        // GET: Korisnici/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Korisnici.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // POST: Korisnici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImeIprezime,brojTelefona,adresa,userName,password,slika")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(osoba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osoba);
        }

        // GET: Korisnici/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Korisnici.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // POST: Korisnici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Osoba osoba = db.Korisnici.Find(id);
            db.Korisnici.Remove(osoba);
            db.SaveChanges();
            return RedirectToAction("Index");
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
