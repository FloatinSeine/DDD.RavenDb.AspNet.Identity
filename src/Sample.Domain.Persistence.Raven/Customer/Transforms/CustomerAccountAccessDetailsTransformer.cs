
using System.Linq;
using Sample.Domain.Model.Customer;
using Raven.Client.Indexes;

namespace Sample.Domain.Persistence.Raven.Customer.Transforms
{
    public class CustomerAccountAccessDetailsTransformer : AbstractTransformerCreationTask<CustomerAccount>
    {
        public CustomerAccountAccessDetailsTransformer()
        {
            TransformResults = accounts => from ca in accounts
                                           select new
                                           {
                                               CustomerId = ca.Id,
                                               LockoutEnabled = ca.IsLocked,
                                               LockedOut = ca.LockedOutEndDate,
                                               AccessFailedCount = ca.Membership.FailedPasswordMatchAttempts,
                                               LastFailureDate = ca.Membership.LastPasswordFailureDate,
                                               TwoFactorEnabled = false
                                           };
        }

    }
}
