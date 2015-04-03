
using System.Security.Claims;
using System.Threading.Tasks;
using Sample.Web.ApplicationServices.Configuration;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Options
{
    public class FacebookAuthentication : Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions
    {
        private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        public FacebookAuthentication()
        {
            Init();
        }

        public void Init()
        {
            AppId = FacebookConfigurationSection.FacebookId;
            AppSecret = FacebookConfigurationSection.FacebookSecret;

            Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider
            {
                OnAuthenticated = (context) =>
                {

                    context.Identity.AddClaim(new Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));

                    foreach (var x in context.User)
                    {
                        var claimType = string.Format("urn:facebook:{0}", x.Key);
                        string claimValue = x.Value.ToString();
                        if (!context.Identity.HasClaim(claimType, claimValue))
                            context.Identity.AddClaim(new Claim(claimType, claimValue, XmlSchemaString, "Facebook"));

                    }
                    return Task.FromResult(0);
                }
            };

            Scope.Add("email");
        }
    }
}
