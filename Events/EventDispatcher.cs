using MDDPlatform.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MDDPlatform.Messages.EventDispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public EventDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task HandleAsync<T>(T @event) where T : class, IEvent
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<T>>();
                await handler.HandleAsync(@event);
            }
        }
    }

}
