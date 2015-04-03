
using Sample.Domain.DDD;

namespace Sample.Domain.Model.Customer.Events
{
    public class ChangeLocalAccountPasswordEvent : DomainEvent<CustomerAccount>
    {
        public LocalMembership Membership { get; private set; }

        public ChangeLocalAccountPasswordEvent(LocalMembership membership)
        {
            Membership = membership;
        }
    }
}
