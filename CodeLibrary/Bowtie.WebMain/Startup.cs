using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bowtie.WebMain.Startup))]
namespace Bowtie.WebMain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
