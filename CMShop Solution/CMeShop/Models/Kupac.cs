using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel;

namespace CMeShop.Models
{
    public class Kupac : Osoba
    {
        [DisplayName("CM Kartica")]
        public bool cmKartica { get; set; }
        [DisplayName("Broj bankovne kartice")]
        public string bankovniRacun { get; set; }
        [DisplayName("Broj CM kartice")]
        public string brojCMkartice { get; set; }
        public int KosaricaID { get; set; }
        public virtual Kosarica Kosarica { get; set; }
        public override void prijava(object user) { }
        public override void registracija() { }
        public void kosarica() { }
    }
}




