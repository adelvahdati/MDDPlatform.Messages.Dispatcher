using MDDPlatform.Messages.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MDDPlatform.Messages.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<TResult> HandleAsync<TResult>(IQuery<TResult> query)
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