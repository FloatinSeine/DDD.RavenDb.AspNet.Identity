using System.Security.Claims;
using Sample.Domain.DDD;

namespace Sample.Domain.Model.Customer.Events
{
    public class AppendClaimEvent : DomainEvent<CustomerAccount>
    {
        public Claim Claim { get; private set; }

        public AppendClaimEvent(Claim claim)
        {
            Claim = claim;
        }
    }
}
