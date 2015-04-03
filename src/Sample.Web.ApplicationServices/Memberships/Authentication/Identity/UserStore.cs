using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Sample.Domain.Model.Customer;
using Sample.Web.ApplicationServices.Memberships.Authentication.Identity.UpdateRules;
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity
{
    public class UserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser, string>, IUserLockoutStore<ApplicationUser, string>, IUserTwoFactorStore<ApplicationUser, string>, IUserLoginStore<ApplicationUser, string>, IUserPhoneNumberStore<ApplicationUser, string>, IUserClaimStore<ApplicationUser, string>
    {
        private bool _disposed = false;
        private IAccountFactory _factory;
        private IQueryCustomerAccounts _query;

        public UserStore(IAccountFactory factory, IQueryCustomerAccounts query)
        {
            _factory = factory;
            _query = query;
        }

        public Task CreateAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var accId = _factory.Create(user.UserName, user.PasswordSalt, user.PasswordHash, string.Empty);
            user.Id = accId;

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var acc = _query.FetchCustomerAccountDetails(userId);
            var user = ApplicationUserDecorator.Decorate(acc);

            return Task.FromResult<ApplicationUser>(user);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var acc = _query.FindByUserName(userName);
            if (acc == null) return Task.FromResult<ApplicationUser>(null);

            var user = ApplicationUserDecorator.Decorate(acc);

            return Task.FromResult<ApplicationUser>(user);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            //Todo - potential issue if change type not recognised and no rule available
            while (user.PropertyChanges.Count > 0)
            {
                user.ApplyRules(user.PropertyChanges.Dequeue());
            }
            return Task.FromResult<object>(null);

            /*
            //ToDo Need to refactor the below with a better implementation
            if (user.Changes.Contains("Password"))
            {
                var cmd = new ChangeCustomerAccountPasswordCommand(user.Id, user.PasswordHash);
                _bus.Send(cmd);
                return Task.FromResult<object>(null);
            }
            else if (user.Changes.Contains("ResetAccessFailedCount"))
            {
                var cmd = new ResetCustomerAccountAccessFailedCountCommand(user.Id);
                _bus.Send(cmd);
                return Task.FromResult<object>(null);

            }
            else if (user.Changes[0].StartsWith("UserLoginInfo"))
            {
                var prov = user.Changes[0].Split('-')[1];
                var login = user.Logins.First(x => x.LoginProvider.Equals(prov));

                var cmd = new AppendCustomerAccountSocialLoginCommand(user.Id, login.LoginProvider, login.ProviderKey);
                _bus.Send(cmd);
                return Task.FromResult<object>(null);
            } else if (user.Changes[0].StartsWith("Claims"))
            {
                var valueType = user.Changes[0].Split('-')[1];

                var claim = user.Claims.First(x => x.ValueType.Equals(valueType));

                var cmd = new AppendCustomerAccountClaimCommand(user.Id, claim);
                _bus.Send(cmd);


                throw new NotImplementedException("Kill Claims" + user.Changes.Count);
            }

            user.ClearChanges();
            throw new NotImplementedException("Dont know what to do");
            */
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //_bus = null;
                    _factory = null;
                    _query = null;
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            var pwd = _query.FetchCustomerAccountPassword(user.Id);
            return Task.FromResult(pwd);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            var p = GetPasswordHashAsync(user);
            var b = !string.IsNullOrEmpty(p.Result);
            return Task.FromResult<bool>(b);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.ChangePassword(passwordHash);
            return Task.FromResult<object>(null);
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            var props = _query.FetchCustomerAccountAccessProperties(user.Id);
            return Task.FromResult(props.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            var props = _query.FetchCustomerAccountAccessProperties(user.Id);
            return Task.FromResult<bool>(props.LockoutEnabled);
        }

        /// <summary>
        /// If End date is in the future user can not successfully sign in. 
        /// If End Date is in the past user is successfully signed in
        /// </summary>
        /// <param name="user">Applicaiton User</param>
        /// <returns>DateTimeOffset when the lockout ends</returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            var props = _query.FetchCustomerAccountAccessProperties(user.Id);
            return Task.FromResult(props.LockoutEndDate.HasValue ? props.LockoutEndDate.Value : DateTimeOffset.MinValue);
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            user.ResetAccessFailedCount();
            return Task.FromResult<object>(null);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            user.AppendSocialLogin(login);
            return Task.FromResult<object>(null);
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            var acc = _query.FindByLinkedAccount(login.LoginProvider, login.ProviderKey);
            if (acc == null) return Task.FromResult<ApplicationUser>(null);

            var user = ApplicationUserDecorator.Decorate(acc);
            return Task.FromResult(user);
            //throw new NotImplementedException("Login" + login.LoginProvider + ", " + login.ProviderKey);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            var logins = _query.FetchCustomerAccountSocialLogins(user.Id);


            return Task.FromResult(logins.Logins);
        }

        public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            return Task.FromResult(user.Mobile);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            user.AppendClaims(claim);
            return Task.FromResult<object>(null);
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var dto = _query.FetchCustomerAccountClaims(user.Id);
            IList<Claim> claims = dto.Claims;
            return Task.FromResult(claims);
        }

        public Task RemoveClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }
    }
}
