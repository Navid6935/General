using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(General.Startup))]
namespace General
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
