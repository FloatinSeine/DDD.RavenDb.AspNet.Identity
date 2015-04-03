
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Providers
{
    public class MobileTokenProvider : PhoneNumberTokenProvider<ApplicationUser>
    {
        public MobileTokenProvider()
        {
            MessageFormat = "Your security code is {0}";
        }
    }
}
