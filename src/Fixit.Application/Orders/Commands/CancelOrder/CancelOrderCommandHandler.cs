using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using MediatR;

namespace Fixit.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : AsyncRequestHandler<CancelOrderCommand>
    {
        private readonly IFixitDbContext _dbContext;

        public CancelOrderCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}