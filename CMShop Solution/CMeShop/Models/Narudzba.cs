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
        public int ID { get; set; }
        public int KosaricaID { get; set; }
        public virtual ICollection<StavkaNarudzbe> listaStavki { get; set; }
        public DateTime datumNarudzbe { get; set; }
        public StatusNarudzbe statusNarudzbe { get; set; }
        public string nacinPlacanja { get; set; }
        public bool finalizirano { get; set; }
    }
}
