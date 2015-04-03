
using System.Security.Claims;

namespace Sample.Domain.Model.Customer.Commands
{
    public class AppendCustomerAccountClaimCommand : CustomerAccountCommand
    {
        public Claim Claim { get; private set; }

        public AppendCustomerAccountClaimCommand(string accountId, Claim claim) : base(accountId)
        {
            Claim = claim;
        }
    }
}
