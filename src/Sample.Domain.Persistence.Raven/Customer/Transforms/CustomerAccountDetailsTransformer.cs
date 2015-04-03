
using System.Linq;
using Sample.Domain.Model.Customer;
using Raven.Client.Indexes;

namespace Sample.Domain.Persistence.Raven.Customer.Transforms
{

    /// <summary>
    /// http://ravendb.net/docs/article-page/3.0/csharp/transformers/creating-and-deploying
    /// Deploy Transformers
    /// </summary>
    public class CustomerAccountDetailsTransformer : AbstractTransformerCreationTask<CustomerAccount>
    {
        public CustomerAccountDetailsTransformer()
        {
            TransformResults = accounts => from ca in accounts
                                           select new 
											{
												Id = ca.Id,
												Password = ca.Membership.Password,
                                                LockedOut = ca.IsLocked,
                                                LockedOutEndDate = ca.LockedOutEndDate,
                                                Email = ca.Email,
                                                Mobile = ca.Mobile
											};
        }
    }
}
