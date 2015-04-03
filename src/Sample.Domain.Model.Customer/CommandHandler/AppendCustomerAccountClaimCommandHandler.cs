
using System;
using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;

namespace Sample.Domain.Model.Customer.CommandHandler
{
    public class AppendCustomerAccountClaimCommandHandler : AbstractCommandHandler, ICommand<AppendCustomerAccountClaimCommand>
    {
        public AppendCustomerAccountClaimCommandHandler(IAccountRepository repository) : base(repository)
        {
        }

        public void Execute(AppendCustomerAccountClaimCommand command)
        {
            if(command == null) throw new ArgumentNullException("command", "Command object can not be null");

            var acc = AccountRepository.Get(command.AccountId);
            acc.AppendClaim(command.Claim);
        }

    }
}
