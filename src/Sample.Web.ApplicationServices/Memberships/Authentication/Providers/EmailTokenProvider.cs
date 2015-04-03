
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Providers
{
    public class EmailTokenProvider : EmailTokenProvider<ApplicationUser>
    {
        public EmailTokenProvider()
        {
            Subject = "Security Code";
            BodyFormat = "Your security code is {0}";
        }
    }
}
