
using Sample.Domain.CQRS.Command;

namespace Sample.Domain.Model.Customer.Commands
{
    public class CreateNewCustomerAccountCommand : ICommand
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public CreateNewCustomerAccountCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
