using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LanguageSchool.Startup))]
namespace LanguageSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
