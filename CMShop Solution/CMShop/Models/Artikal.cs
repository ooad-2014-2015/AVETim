using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.Models
{
    class Artikal : INotifyPropertyChanged
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
        public event PropertyChangedEventHandler PropertyChanged;
        public bool daLiJeDostupan() { return false; }
    }
}
