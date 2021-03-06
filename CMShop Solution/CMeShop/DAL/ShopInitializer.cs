﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMeShop.Models;

namespace CMeShop.DAL
{
    public class ShopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CMeShop.DAL.ShopContext>
    {
        protected override void Seed(CMeShop.DAL.ShopContext context)
        {
            var artikli = new List<CMeShop.Models.Artikal>
            {
                new Artikal {naziv = "Ruz za usne", slika = "~/Content/Slike/karmin.png", cijena = Convert.ToDecimal(7.15), proizvodjac="Ovnak", brojKupljenih=25, opis ="Odlican karmin", rokUpotrebe=DateTime.Now, zaliheStanje=120, zemljaPorijekla="BiH"},
                new Artikal {naziv = "Sampon", slika = "~/Content/Slike/kup.png", cijena = Convert.ToDecimal(2.95), proizvodjac="Oriflame", brojKupljenih=2, opis ="Vrlo ugodnog mirisa", rokUpotrebe=DateTime.Now, zaliheStanje=10, zemljaPorijekla="BiH"}
            };
            artikli.ForEach(abc => context.Artikli.Add(abc));
            context.SaveChanges();
            var vlasnik = new List<Vlasnik>
            {
                new Vlasnik{ role="Vlasnik", userName="vlasnik", adresa ="Grbavicka 14C", brojTelefona = "45532525", ImeIprezime="Vejsil Hrustic", password="vlasnik", }
            };
            vlasnik.ForEach(a => context.Korisnici.Add(a));
            context.SaveChanges();
            context.Kosarice.Add(new Kosarica {  ID=1});
            context.SaveChanges();
            var kupci = new List<Kupac>
            {
                new Kupac{ role="Kupac", adresa="Cengic Vila", bankovniRacun="43141241242151225", brojCMkartice="12/RWQO42", brojTelefona="4214214",  ImeIprezime="Imenko Prezimenko", userName="kupac", password="kupac",  KosaricaID=1}
            };
            kupci.ForEach(a => context.Korisnici.Add(a));
            context.Korisnici.Add(new Dostavljac { brojTelefona = "42121412412", adresa = "Vitez BB", ImeIprezime = "Dostavljac Dostavko", userName = "dostavljac", password = "dostavljac", role = "Dostavljac" });
            context.Korisnici.Add(new Dobavljac { adresa = "Travnicka 43", brojTelefona = "321321312", ImeIprezime = "Dobavni Bavko", password = "dobavljac", userName = "dobavljac", role = "Dobavljac", });
            context.SaveChanges();
        }
    }
}