using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMeShop.Models
{
    public class Kupac : Osoba
    {
        [DisplayName("Broj bankovne kartice")]
        [DataType(DataType.CreditCard)]
        public string bankovniRacun { get; set; }
        [DisplayName("Broj CM kartice")]
        public string brojCMkartice { get; set; }
        public int KosaricaID { get; set; }
        public virtual Kosarica Kosarica { get; set; }
        public bool cmKartica
        {
            get
            {
                return brojCMkartice.Length > 0;
            }
        }
    }
}




