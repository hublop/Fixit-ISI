using MediatR;

namespace Fixit.Shared.CQRS
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
        
    }
}