
namespace CMeShop.Models
{
    public class Vlasnik : Osoba
    {
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public StatistickiPrikaz statistickiPrikaz { get; set; }
        public override void prijava(object user) { }
        public override void registracija() { }
        public void pregledajStatistiku() { }
        public void zaposli() { }
        public void otpusti() { }
        public void izbaciArtikalIzProdaje() { }
        public void dodajArtikalUprodaju() { }
    }
}
