using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using cmShop.Resources;

namespace cmShop
{
    public partial class MainPage : PhoneApplicationPage
    {
        System.Windows.Threading.DispatcherTimer dt = new System.Windows.Threading.DispatcherTimer();
          
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 800); // 500 Milliseconds
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

       
        void dt_Tick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/HomePage.xaml?vrijednost=3", UriKind.Relative));
            dt.Stop();
        }

      /*  private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Katalog.xaml?vrijednost=3", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Info.xaml?vrijednost=3", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           // NavigationService.Navigate(new Uri("/Proizvod.xaml?vrijednost=3", UriKind.Relative));
     
        }*/
       

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}