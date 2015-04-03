
namespace Sample.Domain.Model.Customer.Commands
{


    public class ChangeCustomerAccountPasswordCommand : CustomerAccountCommand
    {
        public string Password { get; private set; }

        public ChangeCustomerAccountPasswordCommand(string accountId, string password) : base(accountId)
        {
            Password = password;
        }
    }
}
