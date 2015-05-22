﻿using CMeShop.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CMeShop.DAL
{
    public class ShopContext : DbContext
    {
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Osoba> Korisnici { get; set; }
        public DbSet<Kosarica> Kosarice { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbi { get; set; }
    }
}