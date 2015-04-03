
using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;
using Microsoft.AspNet.Identity;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity.UpdateRules
{
    public class UserSocialLoginUpdated : CommandDispatchRule
    {
        public UserSocialLoginUpdated(ICommandBus bus) : base(bus)
        {
            
        }

        public override string RuleName
        {
            get { return "AppendSocialLogin"; }
        }

        public override void Apply(IdentityUser user, UserPropertyChange change)
        {
            if (change == null || change.ChangeValue == null) return;

            var login = change.ChangeValue as UserLoginInfo;
            var cmd = new AppendCustomerAccountSocialLoginCommand(user.Id, login.LoginProvider, login.ProviderKey);
            CommandBus.Send(cmd);
        }
    }
}
