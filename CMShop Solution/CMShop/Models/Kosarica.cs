using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.Models
{
    class Kosarica : INotifyPropertyChanged
    {
        public ICollection<Artikal> listaArtikala { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void azurirajListuNarudzbi() { }
        public void azurirajListuZaliha() { }
    }
}
