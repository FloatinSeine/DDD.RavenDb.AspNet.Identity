using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sample.Domain.DDD.Implementation
{
    /// <summary>
    /// https://dddsamplenet.codeplex.com/SourceControl/latest#DDDSample-CQRS-EventSourcing/Domain/Bus.cs
    /// </summary>
    public static class InProcessEventPublisher
    {
        [ThreadStatic]
        private static List<Delegate> _actions;
        private static List<Delegate> Actions
        {
            get { return _actions ?? (_actions = new List<Delegate>()); }
        }

        /// <summary>
        /// Rejestruje prcedure obsługi zdarzenia.
        /// </summary>
        /// <param name="callback">Procedura osbługi zdarzenia.</param>
        /// <returns></returns>
        public static IDisposable Register<TAggregate, TEvent>(Action<TAggregate, TEvent> callback)
            where TAggregate : AggregateRoot
            where TEvent : DomainEvent<TAggregate>
        {
            Actions.Add(callback);
            return new DomainEventRegistrationRemover(() => Actions.Remove(callback));
        }

        /// <summary>
        /// Sygnalizuje zdarzenie.
        /// </summary>
        public static void Publish<TAggregate, TEvent>(TAggregate source, TEvent @event)
            where TAggregate : AggregateRoot
            where TEvent : DomainEvent<TAggregate>
        {
            var registeredHandlers = GetRegisteredEventHandlers<TAggregate, TEvent>();
            
            foreach (var handler in registeredHandlers)
            {
                handler.Handle(source, @event);
            }
            
            foreach (Delegate action in Actions)
            {
                Action<TAggregate, TEvent> typedAction = action as Action<TAggregate, TEvent>;
                if (typedAction != null)
                {
                    typedAction(source, @event);
                }
            }
        }

        private static IEnumerable<IEventHandler<TAggregate, TEvent>> GetRegisteredEventHandlers<TAggregate, TEvent>()
            where TAggregate : AggregateRoot
            where TEvent : DomainEvent<TAggregate>
        {
            var registeredHandlers = DependencyResolver.Current.GetServices<IEventHandler<TAggregate, TEvent>>();
            return registeredHandlers;
        }

        /// <summary>
        /// Klasa pomocnicza.
        /// </summary>
        private sealed class DomainEventRegistrationRemover : IDisposable
        {
            private readonly Action _callOnDispose;

            public DomainEventRegistrationRemover(Action toCall)
            {
                _callOnDispose = toCall;
            }

            public void Dispose()
            {
                _callOnDispose();
            }
        }
    }
}
