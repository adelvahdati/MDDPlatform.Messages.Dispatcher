
using MDDPlatform.Messages.Queries;

namespace MDDPlatform.Messages.QueryDispatchers
{
    public interface IQueryDispatcher 
    {
        Task<TResult> HandleAsync<TResult>(IQuery query);
    }
    
}