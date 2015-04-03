using System;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Sample.Domain.Model.Customer;
using Sample.Domain.Persistence.Raven;

namespace Sample.Web.ApplicationServices.Configuration
{
    public class HealthCheck
    {
        public bool CheckHealth()
        {
            Validate<IRavenSessionFactory>();
            Validate<IQueryCustomerAccounts>();
            Validate<IAccountFactory>();
            Validate<IAuthenticationManager>();

            return true;
        }

        public static void Validate<TService>()
        {
            var service = DependencyResolver.Current.GetService<TService>();
            if (service != null) return;

            throw new Exception("Failed to resolve dependency for " + typeof(TService));
        }
    }
}
