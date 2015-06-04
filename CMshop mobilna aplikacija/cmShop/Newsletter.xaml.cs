using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace cmShop
{
    public partial class Newsletter : PhoneApplicationPage
    {
        System.Windows.Threading.DispatcherTimer dt = new System.Windows.Threading.DispatcherTimer();
          
        public Newsletter()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            using (cmShopBazaContext db = new cmShopBazaContext(cmShopBazaContext.ConnectionString))
            {
                db.CreateIfNotExists();
                try
                {
                  
                   Table<Korisnici> stranice = db.GetTable<Korisnici>();
                    Korisnici k= new Korisnici();
                    k.ImeIPrezime = tekst1.Text;
                    k.Email = tekst2.Text;
                   db.Korisnici.InsertOnSubmit(k);
                   db.SubmitChanges();
                  

                }
                catch (Exception ex)
                {

                }

            }        
                      tekst.Text = "Uspjesno ste uneseni u sistem!";
            dt.Interval = new TimeSpan(0, 0, 0, 0, 500); // 500 Milliseconds
            dt.Tick += new EventHandler(dt_Tick);
                dt.Start();
            


        }
        void dt_Tick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/HomePage.xaml?vrijednost=3", UriKind.Relative));
            dt.Stop();
        }
    }
}