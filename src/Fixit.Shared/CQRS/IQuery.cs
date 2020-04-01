using MediatR;

namespace Fixit.Shared.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
        
    }
}