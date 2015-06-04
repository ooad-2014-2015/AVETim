using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;

namespace cmShop
{
    public partial class Info : PhoneApplicationPage
    {
        public Info()
        {
            InitializeComponent();
            MapLayer layer1 = new MapLayer();

            Pushpin pushpin1 = new Pushpin();

            pushpin1.GeoCoordinate = new GeoCoordinate(43.8587658, 18.4195393);
            pushpin1.Content = "CM Shop";
            MapOverlay overlay1 = new MapOverlay();
            overlay1.Content = pushpin1;
            overlay1.GeoCoordinate = new GeoCoordinate(43.8587658, 18.4195393);
            layer1.Add(overlay1);

            mapa.Layers.Add(layer1);

        }
    }
}