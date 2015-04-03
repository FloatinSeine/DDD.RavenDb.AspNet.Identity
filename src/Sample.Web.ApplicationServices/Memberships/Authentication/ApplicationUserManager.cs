using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sample.Web.ApplicationServices.Configuration;
using Sample.Web.ApplicationServices.Memberships.Authentication.Providers;
using Sample.Web.ApplicationServices.Memberships.Authentication.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;


namespace Sample.Web.ApplicationServices.Memberships.Authentication
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        private static IUserStore<ApplicationUser> CreateUserStore()
        {
            var store = DependencyResolver.Current.GetService<IUserStore<ApplicationUser>>();
            if (store != null) return store;

            //ToDo add instrumentation and direct to healthcheck
            var hb = new HealthCheck();
            hb.CheckHealth();

            return null;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(CreateUserStore());



            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new MobileTokenProvider());
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider());
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            //manager.to

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            Task<IdentityResult> result = base.CreateAsync(user);

            try
            {
                IdentityResult identityResult = await result;
            }
            catch (Exception ex)
            {
                var errors = new List<string>() { ex.Message };
                return IdentityResult.Failed(errors.ToArray());
            }

            return result.Result;
        }

        public override async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            Task<ClaimsIdentity> result = base.CreateIdentityAsync(user, authenticationType);

            try
            {
                ClaimsIdentity claims = await result;

            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong creating claims identity", ex);
            }

            return result.Result;
        }
    }
}
