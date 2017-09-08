using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TennisApp.Startup))]
namespace TennisApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
