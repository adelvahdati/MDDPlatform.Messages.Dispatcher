using MDDPlatform.Messages.Queries;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace MDDPlatform.Messages.QueryDispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
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