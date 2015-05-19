using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class Dobavljac : Osoba
    {
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ListaZaliha listaZaliha { get; set; }
        public override void prijava() { }
        public override void registracija() { }
        public void dopuniListu() { }
    }
}
