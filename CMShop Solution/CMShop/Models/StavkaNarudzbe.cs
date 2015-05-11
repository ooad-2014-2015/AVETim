using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.Models
{
    class StavkaNarudzbe
    {
        public int narudzbaID { get; set; }
        public int artikalID { get; set; }
        public Artikal artikal { get; set; }
        public int kolicina { get; set; }
        public Narudzba narudzba { get; set; }
        public void ukloniStavku() { }
        public void dodajStavku() { }
    }
}
