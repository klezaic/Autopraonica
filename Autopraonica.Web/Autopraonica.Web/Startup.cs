using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Autopraonica.Web.Startup))]
namespace Autopraonica.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
