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
