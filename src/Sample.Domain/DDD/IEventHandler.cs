
namespace Sample.Domain.DDD
{
    public interface IEventHandler<in TAggregate, in TEvent>
        where TAggregate : AggregateRoot
        where TEvent : DomainEvent<TAggregate>
    {
        void Handle(TAggregate source, TEvent @event);
    }
}
