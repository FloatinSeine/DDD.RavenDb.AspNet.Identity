
using Sample.Domain.CQRS.Command;
using Sample.Domain.DDD;
using Sample.Domain.Model.Customer.CommandHandler;
using Sample.Domain.Model.Customer.Commands;
using Sample.Domain.Model.Customer.EventHandlers;
using Sample.Domain.Model.Customer.Events;
using Sample.Domain.Model.Customer.Implementation;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Sample.Domain.Model.Customer.DependencyResolution
{
    public class CustomerAccountRegistry : Registry
    {
        public CustomerAccountRegistry()
        {
            For<IAccountFactory>().Use<CustomerAccountFactory>();
            For<IQueryCustomerAccounts>().Use<QueryCustomerAccounts>();

            For<ICommand<ChangeCustomerAccountPasswordCommand>>(Lifecycles.Unique).Use<ChangeCustomerAccountPasswordCommandHandler>();
            For<ICommand<ResetCustomerAccountAccessFailedCountCommand>>(Lifecycles.Unique).Use<ResetCustomerAccountAccessFailedCountCommandHandler>();
            For<ICommand<AppendCustomerAccountSocialLoginCommand>>(Lifecycles.Unique).Use<AppendCustomerAccountSocialLoginCommandHandler>();
            For<ICommand<AppendCustomerAccountClaimCommand>>(Lifecycles.Unique).Use<AppendCustomerAccountClaimCommandHandler>();

            //For<IEventHandler<CustomerAccount, ChangeLocalAccountPasswordEvent>>().Use<ChangeLocalAccountPasswordEventHandler>();
        }
    }
}
