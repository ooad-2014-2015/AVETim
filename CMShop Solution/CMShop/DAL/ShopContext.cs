using CMShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMShop.DAL
{
    class ShopContext : DbContext
    {
        public DbSet<Artikal> Artikli { get; set; }
    }
}
