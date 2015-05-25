using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class Artikal 
    {
        public int ID { get; set; }
        public string naziv { get; set; }
        public string slika { get; set; }
        public decimal cijena { get; set; }
        public string proizvodjac { get; set; }
        public DateTime rokUpotrebe { get; set; }
        public string zemljaPorijekla { get; set; }
        public int zaliheStanje { get; set; }
        public string opis { get; set; }
        public int brojKupljenih { get; set; }
        public bool daLiJeDostupan() {
            if (zaliheStanje <= 0) return false;
            return true;
        }
    }
}
