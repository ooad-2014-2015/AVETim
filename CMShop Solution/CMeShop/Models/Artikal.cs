using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class Artikal
    {
        public int ID { get; set; }
        [DisplayName("Naziv")]
        [DataType(DataType.Text)]
        public string naziv { get; set; }
        [DisplayName("Slika")]
        [DataType(DataType.ImageUrl)]
        public string slika { get; set; }
        [DisplayName("Cijena")]
        [DataType(DataType.Currency)]
        public decimal cijena { get; set; }
        [DisplayName("Proizvođač")]
        [DataType(DataType.Text)]
        public string proizvodjac { get; set; }
        [DisplayName("Rok upotrebe")]
        [DataType(DataType.Date)]
        public DateTime rokUpotrebe { get; set; }
        [DisplayName("Zemlja porijekla")]
        [DataType(DataType.Text)]
        public string zemljaPorijekla { get; set; }
        [DisplayName("Trenutno stanje")]
        public int zaliheStanje { get; set; }
        [DisplayName("Opis")]
        [DataType(DataType.Text)]
        public string opis { get; set; }
        [DisplayName("Broj kupljenih")]
        public int brojKupljenih { get; set; }
        public bool daLiJeDostupan(int _kolicina)
        {
            if (zaliheStanje - _kolicina < 0) return false;
            return true;
        }
    }
}
