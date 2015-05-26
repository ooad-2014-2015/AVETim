using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMeShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "DodajUKosaricu",                                           // Route name
                "Kosarica/{action}/{id}/{kol}",                            // URL with parameters
                new { controller = "Kosarica", action = "Index" }  // Parameter defaults
            );
            routes.MapRoute(
                "Kosarica",                                           // Route name
                "Kosarica/{action}/{id}",                            // URL with parameters
                new { controller = "Kosarica", action = "Index" }  // Parameter defaults
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
