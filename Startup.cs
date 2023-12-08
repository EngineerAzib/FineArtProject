using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FineArtProject.Startup))]
namespace FineArtProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
