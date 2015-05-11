using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.Models
{
    class Kupac : Osoba
    {
        public bool cmKartica { get; set; }
        public string bankovniRacun { get; set; }
        public string brojCMkartice { get; set; }
        public Kosarica kupacKosarica { get; set; }
        public override void prijava() { }
        public override void registracija() { }
        public void kosarica() { }
    }
}
