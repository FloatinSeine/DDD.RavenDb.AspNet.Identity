
using System;
using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;

namespace Sample.Domain.Model.Customer.CommandHandler
{
    public class ChangeCustomerAccountPasswordCommandHandler : AbstractCommandHandler, ICommand<ChangeCustomerAccountPasswordCommand>
    {

        public ChangeCustomerAccountPasswordCommandHandler(IAccountRepository repository) : base(repository)
        {
        }

        public void Execute(ChangeCustomerAccountPasswordCommand command)
        {
            if (string.IsNullOrEmpty(command.AccountId)) throw new ArgumentNullException("command", "Command object doesnt contain a valid AccountId");
            if (string.IsNullOrEmpty(command.Password)) throw new ArgumentNullException("command", "Command object doesnt contain a valid Password");

            AccountRepository.ChangeCustomerAccountPassword(command.AccountId, command.Password);
        }

    }
}
