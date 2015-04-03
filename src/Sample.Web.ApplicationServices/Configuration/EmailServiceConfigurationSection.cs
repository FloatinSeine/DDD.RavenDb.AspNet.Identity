
using System.Configuration;


namespace Sample.Web.ApplicationServices.Configuration
{
    public class EmailServiceConfigurationSection : ConfigurationSection
    {

        public static bool IsEnabled
        {
            get { return false; }
        }

        public static string SmtpHost
        {
            get { return "localhost"; }
        }

        public static int SmtpPort
        {
            get { return 35; }
        }

        public static string NetworkUser
        {
            get { return ""; }
        }

        public static string NetworkPassword
        {
            get { return ""; }
        }
    }
}
