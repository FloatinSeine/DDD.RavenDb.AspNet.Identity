
using System;

namespace Sample.Domain.CQRS.Command
{
    public interface ICommand
    {
    }

    public interface ICommand<in T> : ICommand, IDisposable
    {
        void Execute(T command);
    }
}
