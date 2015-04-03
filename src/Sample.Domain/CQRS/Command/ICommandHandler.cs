
namespace Sample.Domain.CQRS.Command
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
