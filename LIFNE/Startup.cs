using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LIFNE.Startup))]
namespace LIFNE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
