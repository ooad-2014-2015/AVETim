using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMeShop.ViewModels
{
    public class DobavljacArtikalViewModel
    {
        public int ID { get; set; }
        public string naziv { get; set; }
        public decimal cijena { get; set; }
        public string proizvodjac { get; set; }
        public string zemljaPorijekla { get; set; }
        public int zaliheStanje { get; set; }
        public string opis { get; set; }
        [Range(1, 250)]
        public int kolicina { get; set; }
        public bool updateovano { get; set; }
    }
}