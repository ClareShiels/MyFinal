using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyHappyDays.Startup))]
namespace MyHappyDays
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
