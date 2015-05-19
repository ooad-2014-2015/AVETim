using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMeShop.Startup))]
namespace CMeShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
