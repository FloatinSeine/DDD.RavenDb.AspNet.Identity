
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity
{
    public class IdentityUser : IUser<string>
    {
        //public StringCollection Changes = new StringCollection();
        public Queue<UserPropertyChange> PropertyChanges; 

        public IdentityUser()
        {
            PropertyChanges = new Queue<UserPropertyChange>();
            Logins = new List<UserLoginInfo>();
            Claims = new List<Claim>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public bool IsLockedOut { get; set; }
        public DateTimeOffset? LockedOutEndDate { get; set; }
        public int AccessFailedtCount { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public IList<UserLoginInfo> Logins { get; private set; }
        public IList<Claim> Claims { get; private set; }

        public void ChangePassword(string password)
        {
            PasswordHash = password;
            //Changes.Add("Password");
            PropertyChanges.Enqueue(new UserPropertyChange("ChangePassword", password));
        }

        public void ResetAccessFailedCount()
        {
            AccessFailedtCount = 0;
            //Changes.Add("ResetAccessFailedCount");
            PropertyChanges.Enqueue(new UserPropertyChange("ResetAccessFailedCount", 0));
        }

        public void AppendSocialLogin(UserLoginInfo info)
        {
            Logins.Add(info);
            //Changes.Add("UserLoginInfo-"+info.LoginProvider);
            PropertyChanges.Enqueue(new UserPropertyChange("AppendSocialLogin", info));
        }

        public void AppendClaims(Claim claim)
        {
            Claims.Add(claim);
            //Changes.Add("Claims-" + claim.ValueType);
            PropertyChanges.Enqueue(new UserPropertyChange("AppendClaim", claim));
        }

        public void ClearChanges()
        {
            //Changes.Clear();
        }
    }
}
