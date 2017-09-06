using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lua.Startup))]
namespace lua
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
