
using System.Linq;

namespace Sample.Domain.Model.Customer.Queries
{
    public class FetchCustomerAccountPassword
    {
        public string AccountId { get; set; }

        public FetchCustomerAccountPassword(string accountId)
        {
            AccountId = accountId;
        }

        public string Handle(IQueryable<CustomerAccount> accounts)
        {
            var x = accounts.Where(c => c.Id == AccountId).Select(c => c.Membership.Password);
            return x.ToString();
        }
    }
}
