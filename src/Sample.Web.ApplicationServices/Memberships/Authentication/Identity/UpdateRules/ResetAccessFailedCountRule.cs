using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity.UpdateRules
{
    public class ResetAccessFailedCountRule : CommandDispatchRule
    {
        public ResetAccessFailedCountRule(ICommandBus bus) : base(bus)
        {
            
        }

        public override string RuleName
        {
            get { return "ResetAccessFailedCount"; }
        }

        public override void Apply(IdentityUser user, UserPropertyChange change)
        {
            var cmd = new ResetCustomerAccountAccessFailedCountCommand(user.Id);
            CommandBus.Send(cmd);
        }
    }
}
