using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class StavkaNarudzbe
    {
        public int ID { get; set; }
        public int ArtikalID { get; set; }
        public int NarudzbaID { get; set; }
        public virtual Artikal artikal { get; set; }
        public int kolicina { get; set; }
        public virtual Narudzba narudzba { get; set; }
        public void ukloniStavku() { }
        public void dodajStavku() { }
    }
}
