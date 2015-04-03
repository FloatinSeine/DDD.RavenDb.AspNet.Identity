
namespace Sample.Domain.CQRS.Command
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }
}
