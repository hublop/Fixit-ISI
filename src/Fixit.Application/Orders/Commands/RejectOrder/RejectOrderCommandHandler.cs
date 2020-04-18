using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using MediatR;

namespace Fixit.Application.Orders.Commands.RejectOrder
{
    public class RejectOrderCommandHandler : AsyncRequestHandler<RejectOrderCommand>
    {
        private readonly IFixitDbContext _dbContext;

        public RejectOrderCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task Handle(RejectOrderCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}