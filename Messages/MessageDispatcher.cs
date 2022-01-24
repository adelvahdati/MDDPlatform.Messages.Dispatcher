using MDDPlatform.Messages.Commands;
using MDDPlatform.Messages.Events;
using MDDPlatform.Messages.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MDDPlatform.Messages.MessageDispatcher
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleAsync(ICommand command)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
                await handler.HandleAsync((dynamic)command);
            }
        }

        public async Task HandleAsync(IEvent @event)
        {
            Type type = typeof(IEventHandler<>);
            Type[] typeArgs = { @event.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
                await handler.HandleAsync((dynamic)@event);
            }
        }

        public async Task<TResult> HandleAsync<TResult>(IQuery query)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                Type type = typeof(IQueryHandler<,>);
                Type[] typeArgs = { query.GetType(), typeof(TResult) };

                Type handlerType = type.MakeGenericType(typeArgs);
                dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);

                return await handler.HandleAsync((dynamic) query);

            }
        }
    }
}