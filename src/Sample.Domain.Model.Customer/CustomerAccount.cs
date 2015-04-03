using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Sample.Domain.DDD;
using Sample.Domain.Model.Customer.Events;

namespace Sample.Domain.Model.Customer
{
    public sealed class CustomerAccount : AggregateRoot
    {
        internal CustomerAccount()
        {
            Created = DateTime.UtcNow;
            OAuthMemberships = new List<OAuthMembership>();
            IsLocked = false;
            Claims = new List<Claim>();
        }

        public DateTime Created { get; private set; }
        public string Username { get; private set; }
        public LocalMembership Membership { get; private set; }
        public List<OAuthMembership> OAuthMemberships { get; private set; } 
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public bool IsLocked { get; private set; }
        public DateTimeOffset? LockedOutEndDate { get; private set; }
        public List<Claim> Claims { get; private set; } 


        public void CreateLocalMembership(string email, string password)
        {
            var lm = new LocalMembership(password);
           
            RaiseEvent(this, new CustomerAccountCreatedEvent(email, lm));
        }

        public void ChangeLocalAccountPassword(string password)
        {
            var lm = new LocalMembership(password);

            RaiseEvent(this, new ChangeLocalAccountPasswordEvent(lm));
        }

        public void ResetAccessFailedCount()
        {
            RaiseEvent(this, new ResetAccessFailedCountEvent());
        }

        public void AppendSocialLogin(string provider, string providerKey)
        {
            if (OAuthMemberships.Any(x=>x.Provider == provider && x.ProviderUserId == providerKey)) return;

            var login = new OAuthMembership(provider, providerKey);
            RaiseEvent(this, new AppendSocialLoginEvent(login));
        }

        public void AppendClaim(Claim claim)
        {
            if (Claims.Any(x => x.Issuer == claim.Issuer && x.Type == claim.Type)) return;

            RaiseEvent(this, new AppendClaimEvent(claim));   
        }

        private void OnCustomerAccountCreated(CustomerAccountCreatedEvent @event)
        {
            Email = @event.Email;
            Username = @event.Email;
            Membership = @event.Membership;
        }

        private void OnChangeLocalAccountPassword(ChangeLocalAccountPasswordEvent @event)
        {
            Membership = @event.Membership;
        }

        private void OnResetAccessFailedCount(ResetAccessFailedCountEvent @event)
        {
            Membership.ResetFailedMatchAttempts();
        }

        private void OnAppendSocialLogin(AppendSocialLoginEvent @event)
        {
            OAuthMemberships.Add(@event.Membership);
        }

        private void OnAppendClaim(AppendClaimEvent @event)
        {
            Claims.Add(@event.Claim);
        }
    }
}
