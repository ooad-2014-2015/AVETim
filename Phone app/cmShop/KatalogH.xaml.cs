using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
namespace cmShop
{
    public partial class KatalogH : PhoneApplicationPage
    {
        public KatalogH()
        {
            InitializeComponent();
           /* using (cmShopBazaContext db = new cmShopBazaContext(cmShopBazaContext.ConnectionString))
            {
                db.CreateIfNotExists();
                try
                {
                    Table<Proizvodi> proizvodi = db.GetTable<Proizvodi>();
                    var prQuery = from p in proizvodi.ToList() select p;
                    foreach (var proizvod in prQuery)
                    {
                        PivotItem p = new PivotItem();
                        ProizvodKontrola prKontrola = new ProizvodKontrola();
                        prKontrola.ImeText.Text = proizvod.Cijena + " KM";
                        prKontrola.OpisText.Text = proizvod.Opis;
                        if (proizvod.Slika.ToArray() != null && proizvod.Slika.ToArray() is Byte[])
                        {
                            MemoryStream stream = new MemoryStream(proizvod.Slika.ToArray());
                            BitmapImage image = new BitmapImage();
                            image.SetSource(stream);
                            prKontrola.slikaKontrola.Source = image;
                        }
                        p.Header = proizvod.Ime;
                        p.Content = prKontrola;
                        mojPivot.Items.Add(p);
                    }
                }
                catch (Exception e)
                {

                }
            }*/

        }

        private void Pivot_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}