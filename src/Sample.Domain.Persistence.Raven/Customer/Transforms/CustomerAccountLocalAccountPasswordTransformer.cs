using System.Linq;
using Sample.Domain.Model.Customer;
using Raven.Client.Indexes;

namespace Sample.Domain.Persistence.Raven.Customer.Transforms
{
    public class CustomerAccountLocalAccountPasswordTransformer : AbstractTransformerCreationTask<CustomerAccount>
    {
        public CustomerAccountLocalAccountPasswordTransformer()
        {
            TransformResults = accounts => from ca in accounts
                                           select new 
                                            {
                                                CustomerId = ca.Id,
                                                Password = ca.Membership.Password
                                            };
        }
    }
}
