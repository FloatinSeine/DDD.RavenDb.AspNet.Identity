
using Sample.Domain.Model.Customer.Queries;

namespace Sample.Domain.Model.Customer
{
    public interface IQueryCustomerAccounts : IFindCustomerAccounts, IFetchCustomerAccountDetails
    {

    }
}
