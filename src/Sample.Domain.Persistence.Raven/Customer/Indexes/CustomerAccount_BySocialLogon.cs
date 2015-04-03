
using System.Linq;
using Sample.Domain.Model.Customer;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Sample.Domain.Persistence.Raven.Customer.Indexes
{
    public class CustomerAccount_BySocialLogon : AbstractIndexCreationTask<CustomerAccount, CustomerAccount_BySocialLogon.IndexResults>
    {
        public CustomerAccount_BySocialLogon()
        {
            Map = customers => from customer in customers
                                from membership in customer.OAuthMemberships
                                select new
                                {
                                    customer.Id,
                                    membership.Provider,
                                    membership.ProviderUserId
                                };

            Stores.Add(x => x.Id, FieldStorage.Yes);
            Stores.Add(x => x.Provider, FieldStorage.Yes);
            Stores.Add(x => x.ProviderUserId, FieldStorage.Yes);
        }

        public class IndexResults
        {
            public string Id { get; set; }
            public string Provider { get; set; }
            public string ProviderUserId { get; set; }
        }
    }
}
