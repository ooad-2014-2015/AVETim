using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.Models
{
    class Dobavljac : Osoba
    {
        public ListaZaliha listaZaliha { get; set; }
        public override void prijava() { }
        public override void registracija() { }
        public void dopuniListu() { }
    }
}
