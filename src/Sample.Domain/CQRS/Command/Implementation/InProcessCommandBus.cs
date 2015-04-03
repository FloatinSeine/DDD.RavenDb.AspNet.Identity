using System;
using System.Web.Mvc;
using StructureMap;

namespace Sample.Domain.CQRS.Command.Implementation
{
    public class InProcessCommandBus : ICommandBus
    {
        private readonly IContainer _container;

        public InProcessCommandBus(IContainer container)
        {
            _container = container;
        }

        public void Send<T>(T command) where T : ICommand
        {
            try
            {
                using (var handler = GetCommandHandler<T>())
                {
                    handler.Execute(command);
                }
                /*using (var handler = CreateCommandHandlerInstance(command))
                {
                    handler.Execute(command);
                }*/
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to process command.", ex);
            }
        }

        /*private ICommand<T> CreateCommandHandlerInstance<T>(T command)
        {
            var handler = _container.TryGetInstance<ICommand<T>>();

            if (handler != null) return handler;

            try
            {
                var h = _container.GetInstance<ICommand<T>>();
                _container.AssertConfigurationIsValid();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create Command Handler Interface Instance: " + command + "\n" + ObjectFactory.Container.WhatDoIHave(), ex);
                    
            }
                
            throw new Exception("Failed to create Command Handler Interface Instance: " + command);
        }*/

        private ICommand<TCommand> GetCommandHandler<TCommand>()
            where TCommand : ICommand
        {
            var handler = DependencyResolver.Current.GetService<ICommand<TCommand>>();
            if (handler != null) return handler;

            throw new Exception("Failed to create Command Handler Interface Instance: " + typeof(TCommand));
        }
    }
}
