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
    public class KosaricaController : Controller
    {
        private ShopContext db = new ShopContext();

        public ActionResult Dodaj(int id)
        {
            if (!db.Artikli.Find(id).daLiJeDostupan())
            {
                ViewBag.Poruka = "Navedeni artikal trenutno nije na stanju. Ne možete ga dodati u Vašu košaricu.";
                return View();
            }
            var listaStavki = (List<StavkaKosarice>)Session["StavkeKosarice"];
            listaStavki.Add(new StavkaKosarice { ArtikalID = id, kolicina = 1, KosaricaID = ((Kupac)db.Korisnici.Find(Session["id"])).KosaricaID });
            Session["StavkeKosarice"] = listaStavki;
            //ViewBag.Poruka = "Uspješno ste dodali artikal " + db.Artikli.Find((<))
            return View();
        }
        public ActionResult Finish()
        {
            var listaStavki = (List<StavkaKosarice>)Session["StavkeKosarice"];
            foreach (var item in listaStavki)
            {
                db.StavkeKosarice.Add(item);
                db.Artikli.Find(item.ID).zaliheStanje -= item.kolicina;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        // GET: Kosarica
        public ActionResult Index()
        {
            return View(db.Kosarice.ToList());
        }

        // GET: Kosarica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kosarica kosarica = db.Kosarice.Find(id);
            if (kosarica == null)
            {
                return HttpNotFound();
            }
            return View(kosarica);
        }

        // GET: Kosarica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kosarica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] Kosarica kosarica)
        {
            if (ModelState.IsValid)
            {
                db.Kosarice.Add(kosarica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kosarica);
        }

        // GET: Kosarica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kosarica kosarica = db.Kosarice.Find(id);
            if (kosarica == null)
            {
                return HttpNotFound();
            }
            return View(kosarica);
        }

        // POST: Kosarica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] Kosarica kosarica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kosarica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kosarica);
        }

        // GET: Kosarica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kosarica kosarica = db.Kosarice.Find(id);
            if (kosarica == null)
            {
                return HttpNotFound();
            }
            return View(kosarica);
        }

        // POST: Kosarica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kosarica kosarica = db.Kosarice.Find(id);
            db.Kosarice.Remove(kosarica);
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
