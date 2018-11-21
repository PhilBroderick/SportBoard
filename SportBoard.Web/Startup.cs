using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportBoard.Web.Startup))]
namespace SportBoard.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
