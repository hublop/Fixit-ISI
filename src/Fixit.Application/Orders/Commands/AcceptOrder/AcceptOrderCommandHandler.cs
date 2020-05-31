using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Exceptions;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Orders.Commands.AcceptOrder
{
    public class AcceptOrderCommandHandler : ICommandHandler<AcceptOrderCommand>
    {
        private readonly IFixitDbContext _dbContext;

        public AcceptOrderCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _dbContext.Contractors.FindAsync(request.ContractorId);
            if (contractor == null)
            {
                throw new ContractorDoesNotExistException(request.ContractorId);
            }

            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);
            if (order == null)
            {
                //throw new BadRequestException($"Distributed order does not exist: {request.OrderId}");
                throw new BadRequestException($"Order does not exist: {request.OrderId}");
            }

            if (await _dbContext.OrderOffers.AnyAsync(x =>
                    x.OrderId == request.OrderId && x.ContractorId == request.ContractorId,
                cancellationToken: cancellationToken))
            {
                throw new BadRequestException(
                    $"Contractor: {request.ContractorId} already accepted this order: {request.OrderId}");
            }

            var orderOffer = new OrderOffer
            {
                Comment = request.Comment,
                ContractorId = request.ContractorId,
                OrderId = request.OrderId,
                PredictedPrice = request.PredictedPrice
            };

            await _dbContext.OrderOffers.AddAsync(orderOffer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}