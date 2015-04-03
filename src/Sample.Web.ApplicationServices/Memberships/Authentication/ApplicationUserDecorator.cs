
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Sample.Domain.Model.Customer;
using Sample.Domain.Model.Customer.Dtos;
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication
{
    public class ApplicationUserDecorator
    {
        public static ApplicationUser Decorate(AccountDto account)
        {
            var user = new ApplicationUser
            {
                Id = account.Id,
                PasswordHash = account.Password,
                //PasswordSalt = account.Membership.Salt,
                Mobile = account.Mobile,
                IsLockedOut = account.LockedOut,
                LockedOutEndDate = account.LockedOutEndDate,
                UserName = account.Email,
                TwoFactorEnabled = false,
                Email = account.Email
            };

            //AppendLogins(user, account.);

            return user;
        }

        public static ApplicationUser Decorate(CustomerAccount account)
        {
            if (account == null) return null;

            var user = new ApplicationUser
            {
                Id = account.Id,
                PasswordHash = account.Membership.Password,
                PasswordSalt = account.Membership.Salt,
                Mobile = account.Mobile,
                IsLockedOut = account.IsLocked,
                LockedOutEndDate = account.LockedOutEndDate,
                UserName = account.Username,
                TwoFactorEnabled = false,
                Email = account.Username
            };

            //AppendLogins(user, account.OAuthMemberships);
            //AppendClaims(user, account.Claims);

            return user;
        }

        private static void AppendLogins(ApplicationUser user, IEnumerable<OAuthMembership> memberships)
        {
            foreach (var mem in memberships)
            {
                user.Logins.Add(new UserLoginInfo(mem.Provider, mem.ProviderUserId));
            }
        }

        private static void AppendClaims(ApplicationUser user, IEnumerable<Claim> claims)
        {
            foreach (var c in claims)
            {
                user.Claims.Add(c);
            }
        }
    }
}
