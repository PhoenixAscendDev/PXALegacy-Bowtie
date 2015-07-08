using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bowtie.CustomerWebsite.Startup))]
namespace Bowtie.CustomerWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
