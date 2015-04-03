
using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity.UpdateRules
{
    public class PasswordChangedRule : CommandDispatchRule
    {
        public PasswordChangedRule(ICommandBus bus) : base(bus)
        {
        }

        public override string RuleName
        {
            get { return "ChangePassword"; }
        }

        public override void Apply(IdentityUser user, UserPropertyChange change)
        {
            var s = change.ChangeValue as string;
            var cmd = new ChangeCustomerAccountPasswordCommand(user.Id, s);
            CommandBus.Send(cmd);
        }
    }
}
