using System;
using System.Linq;
using Sample.Domain.CQRS.Command;
using Sample.Domain.Model.Customer.Commands;
using Sample.Domain.Model.Customer.Specifications;

namespace Sample.Domain.Model.Customer.CommandHandler
{
    public class CreateNewCustomerAccountCommandHandler : AbstractCommandHandler, ICommand<CreateNewCustomerAccountCommand>
    {

        public CreateNewCustomerAccountCommandHandler(IAccountRepository repository) : base(repository)
        {
        }

        public void Execute(CreateNewCustomerAccountCommand command)
        {
            var spec = new CustomerAccountHasUsername(command.Email);
            var accs = AccountRepository.FindAll(spec);

            if (accs.Any())
            {
                //Account with username already exists.
                throw new Exception("Can not create CustomerAccount");
            }

            var acc = new CustomerAccount();
            acc.CreateLocalMembership(command.Email, command.Password);
        }
    }
}
