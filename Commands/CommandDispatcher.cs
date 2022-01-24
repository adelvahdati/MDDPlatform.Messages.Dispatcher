using MDDPlatform.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
namespace MDDPlatform.Messages.CommandDispatchers
{
    public class CommandDispatcher : ICommandDispatcher{
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CommandDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleAsync<T>(T command) where T : class, ICommand
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
                await handler.HandleAsync(command);
            }
             
        }
    }
    
}