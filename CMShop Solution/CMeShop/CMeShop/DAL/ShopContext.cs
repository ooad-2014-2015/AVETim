using CMeShop.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CMeShop.DAL
{
    public class ShopContext : DbContext
    {
        public DbSet<Artikal> Artikli { get; set; }
    }
}
