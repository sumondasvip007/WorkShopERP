using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HanifWorkShop.Startup))]
namespace HanifWorkShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
