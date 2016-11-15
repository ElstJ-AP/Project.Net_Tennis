using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BelgianTennisFederation.Startup))]
namespace BelgianTennisFederation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
