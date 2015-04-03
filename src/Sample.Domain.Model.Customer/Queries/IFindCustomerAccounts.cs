
using Sample.Domain.Model.Customer.Dtos;

namespace Sample.Domain.Model.Customer.Queries
{
    public interface IFindCustomerAccounts
    {
        CustomerAccount FindByUserName(string username);
        AccountDto FindByLinkedAccount(string provider, string providerId);
    }
}
