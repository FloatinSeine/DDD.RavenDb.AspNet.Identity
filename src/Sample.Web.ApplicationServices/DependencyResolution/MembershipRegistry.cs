
using System.Web;
using Sample.Web.ApplicationServices.Memberships.Authentication;
using Sample.Web.ApplicationServices.Memberships.Authentication.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StructureMap.Configuration.DSL;

namespace Sample.Web.ApplicationServices.DependencyResolution
{
    public class MembershipRegistry : Registry
    {
        public MembershipRegistry()
        {
            For<IAuthenticationManager>().Use(ctx => HttpContext.Current.GetOwinContext().Authentication);
            For<IUserStore<ApplicationUser>>().Use<UserStore>();
        }
    }
}
