
using Sample.Domain.CQRS.Command;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity.UpdateRules
{
    public abstract class CommandDispatchRule
    {
        protected ICommandBus CommandBus { get; private set; }

        protected CommandDispatchRule(ICommandBus bus)
        {
            CommandBus = bus;
        }

        public abstract string RuleName { get; }
        public abstract void Apply(IdentityUser user, UserPropertyChange change);
    }
}
