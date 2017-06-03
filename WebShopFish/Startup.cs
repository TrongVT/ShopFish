using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebShopFish.Startup))]
namespace WebShopFish
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
