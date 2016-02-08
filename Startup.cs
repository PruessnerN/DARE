using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DARE.Startup))]
namespace DARE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
