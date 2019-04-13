using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeLoanManagementSystem.Startup))]
namespace EmployeeLoanManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
