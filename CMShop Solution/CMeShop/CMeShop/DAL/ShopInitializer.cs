using System;
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
                new Artikal {naziv = "Ruz za usne", slika = "http://www.slike.ba/karmin.jpg", cijena = Convert.ToDecimal(2.15), proizvodjac="Ovnak", brojKupljenih=25, opis ="Odlican karmin", rokUpotrebe=DateTime.Now, zaliheStanje=120, zemljaPorijekla="BiH"},
                new Artikal {naziv = "Sampon", slika = "http://www.slike.ba/sampon.jpg", cijena = Convert.ToDecimal(6.95), proizvodjac="Oriflame", brojKupljenih=2, opis ="Vrlo ugodnog mirisa", rokUpotrebe=DateTime.Now, zaliheStanje=10, zemljaPorijekla="BiH"}
            };
            artikli.ForEach(abc => context.Artikli.Add(abc));
            context.SaveChanges();
        }
    }
}