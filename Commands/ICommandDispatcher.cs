using MDDPlatform.Messages.Commands;
namespace MDDPlatform.Messages.CommandDispatchers
{
    public interface ICommandDispatcher{
        Task HandleAsync<T>(T command) where T : class, ICommand;
    }
}