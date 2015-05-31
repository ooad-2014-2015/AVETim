using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMeShop.Models
{
    public abstract class Osoba
    {
        public int ID { get; set; }
        [DisplayName("Ime i prezime")]
        [DataType(DataType.Text)]
        public string ImeIprezime { get; set; }
        [DisplayName("Broj telefona")]
        [DataType(DataType.PhoneNumber)]
        public string brojTelefona { get; set; }
        [DisplayName("Adresa")]
        [DataType(DataType.Text)]
        public string adresa { get; set; }
        [DisplayName("Username")]
        [DataType(DataType.Text)]
        public string userName { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DisplayName("e-Mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public string role { get; set; }
    }
}