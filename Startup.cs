using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proiect_Rent_A_Car.Startup))]
namespace Proiect_Rent_A_Car
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
