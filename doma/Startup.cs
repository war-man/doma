using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(doma.Startup))]
namespace doma
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
