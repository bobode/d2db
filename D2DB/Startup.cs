using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(D2DB.Startup))]
namespace D2DB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
