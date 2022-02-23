using Microsoft.Extensions.DependencyInjection;

namespace MDDPlatform.Messages.Dispatchers
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageDispatcher(this IServiceCollection services){
            services.AddSingleton<IMessageDispatcher,MessageDispatcher>();
            return services;
        }
        public static IServiceCollection AddCommandDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher,CommandDispatcher>();
            return services;
        }
        public static IServiceCollection AddEventDispatcher(this IServiceCollection services){
            services.AddSingleton<IEventDispatcher,EventDispatcher>();
            return services;
        }
        public static IServiceCollection AddQueryDispatcher(this IServiceCollection services){
            services.AddSingleton<IQueryDispatcher,QueryDispatcher>();
            return services;
        }
    }
}