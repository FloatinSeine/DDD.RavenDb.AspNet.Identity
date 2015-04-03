
using Sample.Domain.Model.Customer.Dtos;
using Sample.Domain.Persistence;

namespace Sample.Domain.Model.Customer
{
    public interface IAccountRepository : IRepository<CustomerAccount>
    {
        AccountDto FetchCustomerAccountDetails(string accountId);
        AccountDto FetchCustomerAccountBySocialLogon(string provider, string providerUserId);
        LocalAccountPasswordDto FetchCustomerAccountLocalAccountPassword(string accountId);
        AccountClaimsDto FetchCustomerAccountClaims(string accountId);
        AccountAccessProprtiesDto FetchCustomerAccountAccessProperties(string accountId);
        AccountLoginsDto FetchCustomerAccountSocialLogins(string accountId);
        void ChangeCustomerAccountPassword(string accountId, string password);
        void ResetCustomerAccountAccessFailedCount(string accountId);

    }
}
