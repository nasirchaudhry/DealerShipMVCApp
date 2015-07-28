using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DealerShipMVCApp.Startup))]
namespace DealerShipMVCApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
