using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NeuroEduTech.Startup))]
namespace NeuroEduTech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
