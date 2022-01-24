using MDDPlatform.Messages.Events;

namespace MDDPlatform.Messages.EventDispatchers
{
    public interface IEventDispatcher{
        Task HandleAsync<T> (T @event) where T: class,IEvent;
    }
}