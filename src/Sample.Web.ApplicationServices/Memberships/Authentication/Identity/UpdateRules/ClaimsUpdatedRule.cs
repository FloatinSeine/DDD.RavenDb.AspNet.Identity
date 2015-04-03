using System.Security.Claims;
using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity.UpdateRules
{
    public class ClaimsUpdatedRule : CommandDispatchRule
    {
        public ClaimsUpdatedRule(ICommandBus bus) : base(bus)
        {
        }

        public override string RuleName
        {
            get { return "AppendClaim"; }
        }

        public override void Apply(IdentityUser user, UserPropertyChange change)
        {
            var claim = change.ChangeValue as Claim;
            var cmd = new AppendCustomerAccountClaimCommand(user.Id, claim);
            CommandBus.Send(cmd);
        }
    }
}
