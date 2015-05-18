using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class Kosarica 
    {
        public ICollection<Artikal> listaArtikala { get; set; }
        public void azurirajListuNarudzbi() { }
        public void azurirajListuZaliha() { }
    }
}
