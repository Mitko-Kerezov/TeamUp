using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamUp.Web.Startup))]
namespace TeamUp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
