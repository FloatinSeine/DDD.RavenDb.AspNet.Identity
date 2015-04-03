using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;

namespace Sample.Domain.Model.Customer.CommandHandler
{
    public class AppendCustomerAccountSocialLoginCommandHandler : AbstractCommandHandler, ICommand<AppendCustomerAccountSocialLoginCommand>
    {
        public AppendCustomerAccountSocialLoginCommandHandler(IAccountRepository repository) : base(repository)
        {
        }

        public void Execute(AppendCustomerAccountSocialLoginCommand command)
        {
            var acc = AccountRepository.Get(command.AccountId);
            acc.AppendSocialLogin(command.Provider, command.ProviderKey);
        }

    }
}
