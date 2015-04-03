using Sample.Domain.DDD;
using Sample.Domain.Model.Customer.Events;

namespace Sample.Domain.Model.Customer.EventHandlers
{
    public class CustomerAccountCreatedEventHandler : IEventHandler<CustomerAccount, CustomerAccountCreatedEvent>
    {
        private readonly IAccountRepository _repository;

        public CustomerAccountCreatedEventHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CustomerAccount source, CustomerAccountCreatedEvent @event)
        {
            _repository.Add(source);
        }
    }
}
