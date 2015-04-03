using System;
using System.Linq;
using System.Linq.Expressions;
using Sample.Domain.CQRS.Specifications;

namespace Sample.Domain.Model.Customer.Specifications
{
    public class CustomerAccountHasLinkedAccount : Specification<CustomerAccount>
    {
        public string Provider { get; private set; }
        public string ProviderId { get; private set; }
        public CustomerAccountHasLinkedAccount(string provider, string providerId)
        {
            Provider = provider;
            ProviderId = providerId;
        }

        public override Expression<Func<CustomerAccount, bool>> ToExpression
        {
            get { return (x => x.OAuthMemberships.Any(y=>y.Provider.Equals(Provider) && y.ProviderUserId.Equals(ProviderId))); }
        }
    }
}
