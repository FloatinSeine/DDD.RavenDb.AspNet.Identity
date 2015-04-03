
namespace Sample.Domain.Model.Customer.Commands
{
    public class AppendCustomerAccountSocialLoginCommand : CustomerAccountCommand
    {
        public string Provider { get; private set; }
        public string ProviderKey { get; private set; }

        public AppendCustomerAccountSocialLoginCommand(string accountId, string provider, string providerKey) : base(accountId)
        {
            Provider = provider;
            ProviderKey = providerKey;
        }
    }
}
