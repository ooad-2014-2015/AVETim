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

        public ActionResult Dodaj(Nullable<int> id, Nullable<int> kol)
        {
            kol = Convert.ToInt32(this.Request.QueryString["kolicina"]);

            if (Session["id"] == null)
            {
                ViewBag.Poruka = "Dodavanje artikala u košaricu mogu izvršiti samo registrovani kupci.";
                return View();
            }
            if (id == null || id < 0 || kol <= 0 || kol == null)
            {
                ViewBag.Poruka = "Dogodila se greška. Neuspješno dodavanje artikla u košaricu.";
                return View();
            }
            Artikal artikal = db.Artikli.Find(id.Value);
            var listaStavki = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (listaStavki.Exists(a => a.ArtikalID == id.Value))
            {
                ViewBag.Poruka = "Navedeni proizvod se već nalazi u vašoj košarici. Ako želite novu količinu, prvo uklonite proizvod iz vaše košarice.";
                return View();
            }
            if (!artikal.daLiJeDostupan(kol.Value))
            {
                ViewBag.Poruka = "Navedeni artikal trenutno nije na stanju. Ne možete ga dodati u Vašu košaricu.";
                return View();
            }
            listaStavki.Add(new StavkaKosarice
            {
                ArtikalID = id.Value,
                kolicina = kol.Value,
                KosaricaID = ((Kupac)db.Korisnici.Find(Session["id"])).KosaricaID,
                kosarica = ((Kupac)db.Korisnici.Find(Session["id"])).Kosarica,
                artikal = (Artikal)db.Artikli.Find(id.Value),
                isporuceno = false
            });
            Session["StavkeKosarice"] = listaStavki;
            ViewBag.Poruka = "Uspješno ste dodali " + kol.Value + " komada artikla " + db.Artikli.Find(id.Value).naziv + ".";
            return View();
        }
        public ActionResult Finish()
        {
            if (Session["StavkeKosarice"] == null) return RedirectToAction("Index", "Home");
            var listaStavki = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (listaStavki.Count == 0) return RedirectToAction("Index", "Home");
            foreach (var item in listaStavki)
            {
                using (var ctx = new ShopContext())
                {
                    ctx.StavkeKosarice.Add(item);
                    var artikal = ctx.Artikli.Find(item.ArtikalID);
                    var kosarica = ctx.Kosarice.Find(item.KosaricaID);
                    artikal.zaliheStanje -= item.kolicina;
                    artikal.brojKupljenih += item.kolicina;
                    ctx.Entry(kosarica).State = EntityState.Modified;
                    ctx.Entry(artikal).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            ViewBag.Poruka = "Uspješno ste izvršili kupovinu u našoj online prodavnici. Pošiljka će vam uskoro biti isporučena na Vašu adresu.";
            Session["StavkeKosarice"] = new List<StavkaKosarice>(); //omogucava novu kupovinu
            return View();
        }
        // GET: Kosarica
        public ActionResult Index()
        {
            if (Session["id"] == null && (string)Session["role"] != "Kupac")
            {
                ViewBag.ErrorModel = "Pregled košarice je omogućen samo prijavljenim kupcima.";
                return View();
            }
            return View((List<StavkaKosarice>)Session["StavkeKosarice"]);
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

        // GET: Kosarica/Delete/5
        public ActionResult Delete(int? id)
        {  //linq da bi se obezbjedilo da samo iz svoje košarice korisnik može ukloniti proizvod
            if (id == null || (List<StavkaKosarice>)Session["StavkeKosarice"] == null || ((List<StavkaKosarice>)Session["StavkeKosarice"]).Where(a => a.ID == id.Value).Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaKosarice stavka = db.StavkeKosarice.Find(id);
            if (stavka == null)
            {
                return HttpNotFound();
            }
            return View(stavka);
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
