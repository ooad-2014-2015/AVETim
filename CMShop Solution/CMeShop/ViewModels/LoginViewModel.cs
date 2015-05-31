using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMeShop.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Korisničko ime")]
        public string userName { get; set; }
        [DisplayName("Lozinka")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}