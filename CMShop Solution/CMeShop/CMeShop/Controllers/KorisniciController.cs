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
            return View(db.Korisnici.ToList());
        }

        // GET: Korisnici/Details/5
        public ActionResult Details(int? id)
        {
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
            return View();
        }

        // POST: Korisnici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ImeIprezime,brojTelefona,adresa,userName,password,slika")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                db.Korisnici.Add(osoba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osoba);
        }

        // GET: Korisnici/Edit/5
        public ActionResult Edit(int? id)
        {
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
