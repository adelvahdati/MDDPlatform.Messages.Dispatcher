using MDDPlatform.Messages.Commands;
using MDDPlatform.Messages.Events;
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.Messages.MessageDispatcher
{
    public interface IMessageDispatcher
    {
        Task HandleAsync(ICommand command);
        Task HandleAsync(IEvent @event);
        Task<TResult> HandleAsync<TResult>(IQuery query);
    }
}