
using Sample.Domain.DDD;

namespace Sample.Domain.Model.Customer.Events
{
    public class AppendSocialLoginEvent : DomainEvent<CustomerAccount>
    {
        public OAuthMembership Membership { get; private set; }

        public AppendSocialLoginEvent(OAuthMembership membership)
        {
            Membership = membership;
        }
    }
}
