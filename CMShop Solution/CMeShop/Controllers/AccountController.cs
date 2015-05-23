﻿using CMeShop.DAL;
using CMeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMeShop.Controllers
{
    public class AccountController : Controller
    {
        private ShopContext db = new ShopContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session["username"] != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kupac kupac)
        {
            var count = db.Korisnici.Where(x => x.userName == kupac.userName && x.password == kupac.password).Count();
            if (count == 0)
            {
                ViewBag.Poruka = "Podaci za prijavu su netačni. Molimo Vas provjerite vaše korisničko ime i lozinku.";
                return View();
            }
            else
            {
                var userFromDb = db.Korisnici.Where(x => x.userName == kupac.userName).First();
                if (userFromDb.role == "Vlasnik") { ViewBag.ErrorModel = "Vlasnik" ; return View(); }
                else if (userFromDb.role == "Dostavljac") { ViewBag.ErrorModel = "Ovdje se mogu prijaviti samo registrovani kupci. Ukoliko se zelite prijaviti kao Dostavljač to možete učiniti "; return View(); }
                else if (userFromDb.role == "Dobavljac") { ViewBag.ErrorModel = "Ovdje se mogu prijaviti samo registrovani kupci. Ukoliko se zelite prijaviti kao Dobavljač to možete učiniti "; return View(); }
                FormsAuthentication.SetAuthCookie(kupac.userName, false);
                Session["username"] = userFromDb.userName;
                Session["id"] = userFromDb.ID;
                Session["role"] = userFromDb.role;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Details()
        {
            if (Session["id"] != null)
            {
                Kupac kupac = (Kupac)db.Korisnici.Find(Session["id"]);
                return View(kupac);
            }
            else
            {
                ViewBag.ErrorPoruka = "Niste logovani. Molimo vas da se prijavite kako bi ste mogli pristupiti vašem računu.";
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
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