using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdeoInterview.Startup))]
namespace IdeoInterview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
