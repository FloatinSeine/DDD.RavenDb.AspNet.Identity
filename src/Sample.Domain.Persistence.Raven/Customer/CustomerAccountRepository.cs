
using System;
using System.Linq;
using Sample.Domain.Model.Customer;
using Sample.Domain.Model.Customer.Dtos;
using Sample.Domain.Model.Customer.Specifications;
using Sample.Domain.Persistence.Raven.Customer.Transforms;
using Raven.Client.Indexes;

namespace Sample.Domain.Persistence.Raven.Customer
{
    public class CustomerAccountRepository : BaseRepository<CustomerAccount>, IAccountRepository
    {
        public CustomerAccountRepository(IRavenSessionFactory factory) : base(factory)
        {
            
        }

        public AccountDto FetchCustomerAccountDetails(string accountId)
        {
            return Get<CustomerAccountDetailsTransformer, AccountDto>(accountId);
        }

        public LocalAccountPasswordDto FetchCustomerAccountLocalAccountPassword(string accountId)
        {
            return Get<CustomerAccountLocalAccountPasswordTransformer, LocalAccountPasswordDto>(accountId);
        }

        public AccountLoginsDto FetchCustomerAccountSocialLogins(string accountId)
        {
            return Get<CustomerAccountSocialLoginsTransformer, AccountLoginsDto>(accountId);
        }

        public AccountClaimsDto FetchCustomerAccountClaims(string accountId)
        {
            return Get<CustomerAccountClaimsTransformer, AccountClaimsDto>(accountId);
        }

        public AccountAccessProprtiesDto FetchCustomerAccountAccessProperties(string accountId)
        {
            return Get<CustomerAccountAccessDetailsTransformer, AccountAccessProprtiesDto>(accountId);
        }

        public AccountDto FetchCustomerAccountBySocialLogon(string provider, string providerUserId)
        {
            var spec = new CustomerAccountHasLinkedAccount(provider, providerUserId);
            //var dtos = FindAll<CustomerAccount_BySocialLogon, AccountDto>(spec).ToArray();
            var dtos = FindAll<AccountDto>(spec).ToArray();
            if (dtos.Count() > 1)
            {
                throw new Exception("Multiple Accounts found: " + dtos.Count());
            }
            var acc = dtos.SingleOrDefault();
            return acc;
        }

        public TDto FetchCustomerAccountDetails<TTransform, TDto>(string accountId)
            where TTransform : AbstractTransformerCreationTask<CustomerAccount>, new()
            where TDto : class
        {
            return Get<TTransform, TDto>(accountId);
        }

        public void ChangeCustomerAccountPassword(string accountId, string password)
        {
                var acc = Get(accountId);
                acc.ChangeLocalAccountPassword(password);
        }

        public void ResetCustomerAccountAccessFailedCount(string accountId)
        {
            var acc = Get(accountId);
            acc.ResetAccessFailedCount();
        }
    }
}
