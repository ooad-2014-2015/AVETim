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
    public partial class Katalog : PhoneApplicationPage
    {
        public Katalog()
        {
            InitializeComponent();
            using (cmShopBazaContext db = new cmShopBazaContext(cmShopBazaContext.ConnectionString))
            {
                db.CreateIfNotExists();
                try
                {
                    Table<Stranice> stranice = db.GetTable<Stranice>();
                    var prQuery = from s in stranice.ToList() select s;
                    foreach (var stranica in prQuery)
                    {
                        PivotItem p = new PivotItem();
                        ProizvodKontrola prKontrola = new ProizvodKontrola();
                         if (stranica.Slika.ToArray() != null && stranica.Slika.ToArray() is Byte[])
                        {
                            MemoryStream stream = new MemoryStream(stranica.Slika.ToArray());
                            BitmapImage image = new BitmapImage();
                            image.SetSource(stream);
                            prKontrola.slikaKontrola.Source = image;
                        }
                        p.Content = prKontrola;
                        mojPivot.Items.Add(p);
                    }
                }
                catch (Exception e)
                {

                }
            }
            
        }

        private void Pivot_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}