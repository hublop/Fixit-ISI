using System;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using MediatR;

namespace Fixit.Application.Orders.Commands.AcceptOrder
{
    public class AcceptOrderCommandHandler : AsyncRequestHandler<AcceptOrderCommand>
    {
        private readonly IFixitDbContext _dbContext;

        public AcceptOrderCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}