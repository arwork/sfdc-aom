using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aom.Mvc.Startup))]
namespace Aom.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
