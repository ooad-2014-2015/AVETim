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
using PagedList;

namespace CMeShop.Controllers
{
    public class HomeController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Home
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var artikli = from s in db.Artikli
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                artikli = artikli.Where(s => s.naziv.Contains(searchString)
                                       || s.opis.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    artikli = artikli.OrderByDescending(s => s.naziv);
                    break;
                default:  // Name ascending 
                    artikli = artikli.OrderBy(s => s.naziv);
                    break;
            }

            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(artikli.ToPagedList(pageNumber, pageSize));
        }


        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["id"] == null || Session["StavkeKosarice"] == null)
            {
                ViewBag.Poruka = "Kupovinu artikala i dodavanje artikala u košaricu mogu izvršiti samo prijavljeni kupci.";
                return View();
            }
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

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
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

        // GET: Home/Edit/5
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

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
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

        // POST: Home/Delete/5
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
