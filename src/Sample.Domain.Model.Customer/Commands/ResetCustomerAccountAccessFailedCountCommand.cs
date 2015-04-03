
namespace Sample.Domain.Model.Customer.Commands
{
    public class ResetCustomerAccountAccessFailedCountCommand : CustomerAccountCommand
    {
        public ResetCustomerAccountAccessFailedCountCommand(string accountId) : base(accountId)
        {

        }
    }
}
