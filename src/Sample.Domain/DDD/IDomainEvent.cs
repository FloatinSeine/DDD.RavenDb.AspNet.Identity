
using System;

namespace Sample.Domain.DDD
{
    public interface IDomainEvent
    {
    }

    /*public interface IDomainEvent<in T> : IDomainEvent
    {
        void Handle(T @event);
    }*/

    [Serializable]
    public abstract class DomainEvent<T> where T : AggregateRoot
    {
        
    }
}
