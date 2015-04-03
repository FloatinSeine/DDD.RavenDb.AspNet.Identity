
using System.Configuration;


namespace Sample.Web.ApplicationServices.Configuration
{
    public class FacebookConfigurationSection : ConfigurationSection
    {
        public static string FacebookId
        {
            get { return ConfigurationManager.AppSettings.Get("FacebookAppId"); }
        }

        public static string FacebookSecret
        {
            get { return ConfigurationManager.AppSettings.Get("FacebookAppSecret"); }
        }
    }
}
