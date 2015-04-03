
using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;

namespace Sample.Domain.Model.Customer.CommandHandler
{
    public class ResetCustomerAccountAccessFailedCountCommandHandler : AbstractCommandHandler, ICommand<ResetCustomerAccountAccessFailedCountCommand>
    {

        public ResetCustomerAccountAccessFailedCountCommandHandler(IAccountRepository repository) : base(repository)
        {
        }

        public void Execute(ResetCustomerAccountAccessFailedCountCommand command)
        {
            var acc = AccountRepository.Get(command.AccountId);
            acc.ResetAccessFailedCount();
        }

    }
}
