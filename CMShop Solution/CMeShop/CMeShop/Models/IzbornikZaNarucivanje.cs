﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeShop.Models
{
    public class IzbornikZaNarucivanje : PreglednikProizvoda
    {
        public StatistickiPrikaz statPrikaz { get; set; }
        public void dodajUKosaricu() { }
        public void azurirajStatistickiPrikaz() { }
    }
}
