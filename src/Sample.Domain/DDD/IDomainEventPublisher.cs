
namespace Sample.Domain.DDD
{
    public interface IDomainEventPublisher
    {
        void Publish<T>(T domainEvent);
    }
}
