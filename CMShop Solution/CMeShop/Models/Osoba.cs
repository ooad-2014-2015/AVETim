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
        public int ID { get; set; }
        [DisplayName("Ime i prezime")]
        public string ImeIprezime { get; set; }
        [DisplayName("Broj telefona")]
        public string brojTelefona { get; set; }
        [DisplayName("Adresa")]
        public string adresa { get; set; }
        [DisplayName("Username")]
        public string userName { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }
        [DisplayName("Slika")]
        public string slika { get; set; }
        public string role { get; set; }
        public abstract void prijava(object user);
        public abstract void registracija();
    }
}