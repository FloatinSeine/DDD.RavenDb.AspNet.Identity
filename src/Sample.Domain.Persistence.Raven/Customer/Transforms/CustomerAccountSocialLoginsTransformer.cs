using System.Linq;
using Sample.Domain.Model.Customer;
using Raven.Client.Indexes;

namespace Sample.Domain.Persistence.Raven.Customer.Transforms
{
    public class CustomerAccountSocialLoginsTransformer : AbstractTransformerCreationTask<CustomerAccount>
    {
        public CustomerAccountSocialLoginsTransformer()
        {
            TransformResults = accounts => from ca in accounts
                                           select new
                                           {
                                               CustomerId = ca.Id,
                                               Logins = ca.OAuthMemberships
                                           };
        }
    }
}
