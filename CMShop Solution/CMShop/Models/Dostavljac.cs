using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.Models
{
    class Dostavljac : Osoba
    {
        public ListaNarudzbi listaNarudzbi { get; set; }
        public override void prijava() { }
        public override void registracija() { }
        public void azurirajListu() { }
    }
}
