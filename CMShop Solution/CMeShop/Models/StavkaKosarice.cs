using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class StavkaKosarice
    {
        public int ID { get; set; }
        public int ArtikalID { get; set; }
        public int KosaricaID { get; set; }
        public virtual Artikal artikal { get; set; }
        public int kolicina { get; set; }
        public virtual Kosarica kosarica { get; set; }
        public bool isporuceno { get; set; }
        public decimal ukupnaCijena {
            get
            {
                return  kolicina;
            }
        }
        public void ukloniStavku() { }
        public void dodajStavku() { }
    }
}
