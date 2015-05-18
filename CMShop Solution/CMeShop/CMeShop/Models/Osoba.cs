using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public abstract class Osoba 
    {
        public string ImeIprezime { get; set; }
        public string brojTelefona { get; set; }
        public string adresa { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string slika { get; set; }
        public abstract void prijava();
        public abstract void registracija();
    }
}
