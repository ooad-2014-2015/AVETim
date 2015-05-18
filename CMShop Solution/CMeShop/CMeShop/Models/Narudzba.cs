using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class Narudzba
    {
        public enum StatusNarudzbe
        { ODOBRENO, OTKAZANO };
        public ICollection<StavkaNarudzbe> listaStavki { get; set; }
        public DateTime datumNarudzbe { get; set; }
        public int brojNarudzbe { get; set; }
        public StatusNarudzbe statusNarudzbe { get; set; }
        public string nacinPlacanja { get; set; }
    }
}
