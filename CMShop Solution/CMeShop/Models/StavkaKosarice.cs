using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class StavkaKosarice
    {
        public enum NacinPlacanja
        { Karticom, Pouzecem, NotSet};
        public int ID { get; set; }
        public int ArtikalID { get; set; }
        public int KosaricaID { get; set; }
        public virtual Artikal artikal { get; set; }
        public int kolicina { get; set; }
        public virtual Kosarica kosarica { get; set; }
        public string adresa { get; set; }
        public string imeKupca { get; set; }
        public NacinPlacanja nacinPlacanja { get; set; }
        public DateTime datumKreiranja { get; set; }
        public bool isporuceno { get; set; }
        public decimal UkupnaCijena { get; set; }
        public decimal obracunajCijenuSaPopustom() //5% u slucaju da posjeduje potrosacku karticu
        {
            return UkupnaCijena - UkupnaCijena * (decimal)0.5;
        }
    }
}
