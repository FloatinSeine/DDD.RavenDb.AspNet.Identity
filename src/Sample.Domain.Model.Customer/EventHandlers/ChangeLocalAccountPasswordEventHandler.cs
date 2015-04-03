
using Sample.Domain.DDD;
using Sample.Domain.Model.Customer.Events;

namespace Sample.Domain.Model.Customer.EventHandlers
{
    public class ChangeLocalAccountPasswordEventHandler : IEventHandler<CustomerAccount, ChangeLocalAccountPasswordEvent>
    {
 

        public void Handle(CustomerAccount source, ChangeLocalAccountPasswordEvent @event)
        {
            //ToDo do some work following domain object update
        }
    }
}
