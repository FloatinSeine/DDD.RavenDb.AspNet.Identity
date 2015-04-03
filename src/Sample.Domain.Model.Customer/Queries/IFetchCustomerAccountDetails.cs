
using Sample.Domain.Model.Customer.Dtos;

namespace Sample.Domain.Model.Customer.Queries
{
    public interface IFetchCustomerAccountDetails
    {
        string FetchCustomerAccountPassword(string accountId);
        AccountDto FetchCustomerAccountDetails(string accountId);
        AccountClaimsDto FetchCustomerAccountClaims(string accountId);
        AccountAccessProprtiesDto FetchCustomerAccountAccessProperties(string accountId);
        AccountLoginsDto FetchCustomerAccountSocialLogins(string accountId);
    }
}
