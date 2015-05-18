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
    [Authorize(Roles="Admin")]
    public class ArtikliController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Artikli
        public ActionResult Index()
        {
            return View(db.Artikli.ToList());
        }

        // GET: Artikli/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikli.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return View(artikal);
        }

        // GET: Artikli/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artikli/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,naziv,slika,cijena,proizvodjac,rokUpotrebe,zemljaPorijekla,zaliheStanje,opis,brojKupljenih")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Artikli.Add(artikal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artikal);
        }

        // GET: Artikli/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikli.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return View(artikal);
        }

        // POST: Artikli/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,naziv,slika,cijena,proizvodjac,rokUpotrebe,zemljaPorijekla,zaliheStanje,opis,brojKupljenih")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikal);
        }

        // GET: Artikli/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikal artikal = db.Artikli.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return View(artikal);
        }

        // POST: Artikli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artikal artikal = db.Artikli.Find(id);
            db.Artikli.Remove(artikal);
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
