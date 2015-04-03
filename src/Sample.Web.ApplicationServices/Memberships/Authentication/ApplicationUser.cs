using System.Security.Claims;
using System.Threading.Tasks;
using Sample.Web.ApplicationServices.Memberships.Authentication.Identity;
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
