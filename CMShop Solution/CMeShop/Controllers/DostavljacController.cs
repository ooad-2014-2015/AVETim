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
using System.Drawing.Printing;
using System.Drawing;
using System.IO;

namespace CMeShop.Controllers
{
    public class DostavljacController : Controller
    {
        private ShopContext db = new ShopContext();

        [HttpPost]
        public ActionResult Index(List<StavkaKosarice> updateovaneStavke)
        {
            foreach (var item in updateovaneStavke)
            {
                if (item.isporuceno == true) //doslo je do izmjene
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
            var stavke = db.StavkeKosarice.Include(a => a.artikal).Where(b => b.isporuceno == false);
            return View(stavke.ToList());
        }

        public ActionResult Details(int? id)
        {
            if ((string)Session["role"] != "Dostavljac" && (string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            if (Session["id"] != null && (string)Session["role"] == "Dostavljac")
            {
                Dostavljac dostavljac = (Dostavljac)db.Korisnici.Find(Session["id"]);
                return View(dostavljac);
            }
            else if (id != null && (string)Session["role"] == "Vlasnik")
            {
                Dostavljac dostavljac = (Dostavljac)db.Korisnici.Find(id);
                return View(dostavljac);
            }
            else
            {
                ViewBag.ErrorPoruka = "Niste logovani. Molimo vas da se prijavite kako bi ste mogli pristupiti vašem računu.";
                return View();
            }
        }
        /* Printer externi uređaj */
        //
        private Font printFont;
        private StreamReader streamToPrint;

        public ActionResult Printaj()
        {
            NapuniDatoteku("C:\\izvjestaj.txt");
            streamToPrint = new StreamReader("C:\\izvjestaj.txt");
            try
            {
                printFont = new Font("Arial", 10);
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = "\\\\WIN-TPP7SQ553AD\\Print as a PDF"; //Ime računara \\ Ime stampaca
                pd.PrintPage += new PrintPageEventHandler(SadrzajZaPrintanje);
                pd.Print();
            }
            finally
            {
                streamToPrint.Close();
            }
            return RedirectToAction("Index", "Home");
        }

        public void NapuniDatoteku(string putanja)
        {
            using (TextWriter writer = System.IO.File.CreateText(putanja))
            {
                var listaDostavljaca = db.StavkeKosarice.Where(a => a.isporuceno == false).Include(b => b.artikal);
                writer.WriteLine("Naziv       Kolicina      Cijena      Adresa          Ime i prezime      Nacin placanja       Datum kupovine");
                foreach (var item in listaDostavljaca)
                {
                    writer.WriteLine("{0}     {1}      {2}      {3}      {4}      {5}      {6}", item.artikal.naziv, item.kolicina, item.UkupnaCijena, item.adresa, item.imeKupca, item.nacinPlacanja, item.datumKreiranja);
                }
            }
        }

        public void SadrzajZaPrintanje(object sender, PrintPageEventArgs ev)
        {
            double brLinijaPoStranici = 0;
            double yPos = 0;
            int count = 0;
            string linijaTeksta = null;
            brLinijaPoStranici = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);
            while (count < brLinijaPoStranici &&
               ((linijaTeksta = streamToPrint.ReadLine()) != null))
            {
                yPos = ev.MarginBounds.Top + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(linijaTeksta, printFont, Brushes.Black, ev.MarginBounds.Left, ev.MarginBounds.Top);
                count++;
            }
            if (linijaTeksta != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }
        // kraj koda za printer


        public ActionResult Delete(int? id)
        {
            if ((string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dostavljac user = (Dostavljac)db.Korisnici.Find(id);
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
            Dostavljac user = (Dostavljac)db.Korisnici.Find(id);
            db.Korisnici.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (!((string)Session["role"] == "Dostavljac" && id.Value == (int)Session["id"]) && (string)Session["role"] != "Vlasnik") return View("~/Views/Shared/Error.cshtml");
            ViewBag.Poruka = "Možete izmjeniti podatke, samo kada ste prijavljeni, i to od vlastitog računa, ili ako ste Vlasnik.";
            return View((Dostavljac)db.Korisnici.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Dostavljac dostavljac)
        {
            var userFromDb = (Dostavljac)db.Korisnici.Find(dostavljac.ID);
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
