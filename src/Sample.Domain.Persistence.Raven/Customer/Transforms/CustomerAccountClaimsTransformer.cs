using System.Linq;
using Sample.Domain.Model.Customer;
using Raven.Client.Indexes;

namespace Sample.Domain.Persistence.Raven.Customer.Transforms
{
    public class CustomerAccountClaimsTransformer : AbstractTransformerCreationTask<CustomerAccount>
    {
        public CustomerAccountClaimsTransformer()
        {
            TransformResults = accounts => from ca in accounts
                                           select new
                                           {
                                               CustomerId = ca.Id,
                                               ca.Claims
                                           };
        }
    }
}
