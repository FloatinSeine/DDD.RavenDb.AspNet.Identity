using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sample.Domain.CQRS.Command;

namespace Sample.Web.ApplicationServices.Memberships.Authentication.Identity.UpdateRules
{
    public static class RulesEngine
    {
        public static void ApplyRules(this IdentityUser user, UserPropertyChange change)
        {

            var rules = FetchMatchingRules(change.ChangeType);

            foreach (var r in rules)
            {
                r.Apply(user, change);
            }

        }

        private static IEnumerable<CommandDispatchRule> FetchMatchingRules(string type)
        {
            var rules = Rules().Where(r => r.RuleName.Equals(type)).ToArray();

            if (!rules.Any())
            {
                throw new Exception("No rules exist for change type " + type);
            }
            return rules;
        }

        private static IEnumerable<CommandDispatchRule> Rules()
        {
            var bus = FetchCommandBus();
            yield return (new PasswordChangedRule(bus));
            yield return (new ResetAccessFailedCountRule(bus));
            yield return (new UserSocialLoginUpdated(bus));
            yield return (new ClaimsUpdatedRule(bus));
        }

        private static ICommandBus FetchCommandBus()
        {
            var bus = DependencyResolver.Current.GetService<ICommandBus>();
            return bus;
        }
    }
}
