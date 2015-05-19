using CMeShop.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMeShop.Models
{
    public class Kosarica 
    {
        [Key]
        public int ID { get; set; }
        public virtual ICollection<Narudzba> Narudzbe { get; set; }
    }
}
