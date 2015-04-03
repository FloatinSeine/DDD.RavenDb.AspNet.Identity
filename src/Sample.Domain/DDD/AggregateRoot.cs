using System;
using System.Collections.Generic;
using System.Reflection;
using Sample.Domain.DDD.Implementation;

namespace Sample.Domain.DDD
{
    [Serializable]
    public abstract class AggregateRoot : Entity
    {
        private List<object> _events;

        public DateTime Version { get; private set; }

        protected AggregateRoot()
        {
            UpdateVersion();
        }

        ICollection<object> Events
        {
            get { return _events ?? (_events = new List<object>()); }
        }

        void LoadFromEventStream(IEnumerable<object> events)
        {
            foreach (object @event in events)
            {
                Apply(@event);
            }
        }

        protected void RaiseEvent<TAggregate, TEvent>(TAggregate @this, TEvent @event)
            where TAggregate : AggregateRoot
            where TEvent : DomainEvent<TAggregate>
        {
            Apply(@event);
            this.Events.Add(@event);
            UpdateVersion();
            InProcessEventPublisher.Publish(@this, @event);
        }

        private void Apply(object @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException("event");
            }
            string eventTypeName = @event.GetType().Name;
            int suffixIndex = eventTypeName.LastIndexOf("Event");
            if (suffixIndex <= 0)
            {
                throw new InvalidOperationException("Invalid event name: " + eventTypeName);
            }
            string methodName = "On" + eventTypeName.Substring(0, suffixIndex);
            MethodInfo methodInfo = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            methodInfo.Invoke(this, new[] { @event });
        }

        protected void UpdateVersion()
        {
            Version = DateTime.UtcNow;
        }
    }
}
