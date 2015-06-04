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

        [HttpPost]
        public ActionResult Zavrsi(Nullable<StavkaKosarice.NacinPlacanja> nacinPl)
        {
            List<StavkaKosarice> stavke = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (stavke != null && stavke.Count > 0 && stavke[0].nacinPlacanja == StavkaKosarice.NacinPlacanja.NotSet)
            {
                foreach (var item in (List<StavkaKosarice>)Session["StavkeKosarice"])
                {
                    item.nacinPlacanja = nacinPl.Value;
                }
                return RedirectToAction("Finish", "Kosarica");
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        public ActionResult History()
        {
            List<StavkaKosarice> stavke = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (stavke == null) return View("~/Views/Shared/Error.cshtml"); //znači da nije "Kupac"
            var currentKupac = (Kupac)db.Korisnici.Find((int)Session["id"]);
            var lista = db.StavkeKosarice.Where(a => (a.isporuceno == true && a.KosaricaID == currentKupac.KosaricaID)).Include(b => b.artikal).ToList();
            return View(lista);
        }

        public ActionResult Zavrsi()
        {
            List<StavkaKosarice> stavke = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (stavke == null || stavke.Count == 0) return View("~/Views/Shared/Error.cshtml");
            ViewBag.Poruka = "Kako bi ste završili kupovinu, odaberite način plaćanja. U slučaju da posjedujete CM potrošačku karticu, bit će vam obračunat popust od 5%.";
            return View();
        }

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
                adresa = ((Kupac)db.Korisnici.Find(Session["id"])).adresa,
                imeKupca = ((Kupac)db.Korisnici.Find(Session["id"])).ImeIprezime,
                UkupnaCijena = (decimal)(artikal.cijena*kol.Value),
                nacinPlacanja = StavkaKosarice.NacinPlacanja.NotSet,
                isporuceno = false
            });
            Session["StavkeKosarice"] = listaStavki;
            ViewBag.Poruka = "Uspješno ste dodali " + kol.Value + " komada artikla " + db.Artikli.Find(id.Value).naziv + ".";
            return View();
        }
        public ActionResult Finish() //konačni završetak narudzbe i finalizacija kupovine
        {
            var listaStavki = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (listaStavki == null || listaStavki.Count == 0) return View("~/Views/Shared/Error.cshtml");
            foreach (var item in listaStavki)
            {
                using (var ctx = new ShopContext())
                {
                    item.datumKreiranja = DateTime.Now;
                    Kupac kup = (Kupac)db.Korisnici.Find(Session["id"]);
                    if(kup.cmKartica)
                    {
                        item.UkupnaCijena = item.obracunajCijenuSaPopustom(); // popust od 5% za vlasnike CM kartice
                    }
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
            var lista = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (lista == null)
            {
                return HttpNotFound();
            }
            return View(lista[id.Value]);
        }

        // GET: Kosarica/Delete/5
        public ActionResult Delete(int? id)
        {  //linq da bi se obezbjedilo da samo iz svoje košarice korisnik može ukloniti proizvod
            if (id == null || (List<StavkaKosarice>)Session["StavkeKosarice"] == null || ((List<StavkaKosarice>)Session["StavkeKosarice"]).Where(a => a.ID == id.Value).Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lista = (List<StavkaKosarice>)Session["StavkeKosarice"];
            if (lista == null)
            {
                return HttpNotFound();
            }
            return View(lista[id.Value]);
        }

        // POST: Kosarica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var lista = (List<StavkaKosarice>)Session["StavkeKosarice"];
            lista.Remove(lista[id]);
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
