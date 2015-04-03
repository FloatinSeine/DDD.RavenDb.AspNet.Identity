using System;

namespace Sample.Domain.DDD
{

    public abstract class Entity
    {

        protected Entity()
        {
            Id = NextId();
            Status = EntityStatus.Active;
        }

        public string Id { get; protected set; }
        public EntityStatus Status { get; private set; }

        protected virtual string NextId()
        {
            return (GetType().Name + "/" + Guid.NewGuid());
        }

        public void MarkAsRemoved()
        {
            Status = EntityStatus.Archived;
        }
    }
}
