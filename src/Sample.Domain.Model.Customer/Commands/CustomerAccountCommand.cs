
using Sample.Domain.CQRS.Command;

namespace Sample.Domain.Model.Customer.Commands
{
    public abstract class CustomerAccountCommand : ICommand
    {
        public string AccountId { get; private set; }

        protected CustomerAccountCommand(string accountId)
        {
            AccountId = accountId;
        }
    }
}
