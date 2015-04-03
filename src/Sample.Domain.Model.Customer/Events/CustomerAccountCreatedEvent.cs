
using Sample.Domain.DDD;

namespace Sample.Domain.Model.Customer.Events
{
    public class CustomerAccountCreatedEvent : DomainEvent<CustomerAccount>
    {
        public string Email { get; private set; }
        public LocalMembership Membership { get; private set; }

        public CustomerAccountCreatedEvent(string email, LocalMembership membership)
        {
            Email = email;
            Membership = membership;
        }
    }
}
