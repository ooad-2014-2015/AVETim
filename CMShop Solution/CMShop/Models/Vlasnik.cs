using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.Models
{
    class Vlasnik : Osoba
    {
        public StatistickiPrikaz statistickiPrikaz { get; set; }
        public override void prijava() { }
        public override void registracija() { }
        public void pregledajStatistiku() { }
        public void zaposli() { }
        public void otpusti() { }
        public void izbaciArtikalIzProdaje() { }
        public void dodajArtikalUprodaju() { }
    }
}
