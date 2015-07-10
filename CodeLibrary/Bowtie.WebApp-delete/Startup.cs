using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bowtie.WebApp.Startup))]
namespace Bowtie.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
