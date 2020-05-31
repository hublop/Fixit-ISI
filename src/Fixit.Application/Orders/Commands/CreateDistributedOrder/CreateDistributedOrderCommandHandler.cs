using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Exceptions;
using Fixit.Application.Customers.Exceptions;
using Fixit.Application.Orders.Events.OrderWithPhotosAdded;
using Fixit.Domain.Entities;
using Fixit.EventBus.Abstractions;
using Fixit.Shared.CQRS;
using MediatR;

namespace Fixit.Application.Orders.Commands.CreateDistributedOrder
{
    public class CreateDistributedOrderCommandHandler : ICommandHandler<CreateDistributedOrderCommand>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IEventBus _eventBus;

        public CreateDistributedOrderCommandHandler(IFixitDbContext dbContext, IEventBus eventBus)
        {
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateDistributedOrderCommand request, CancellationToken cancellationToken)
        {
            var subcategory = await _dbContext.Subcategories.FindAsync(request.SubcategoryId);
            if (subcategory == null)
            {
                throw new SubcategoryDoesNotExistException(request.SubcategoryId);
            }

            var customer = await _dbContext.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                throw new CustomerDoesNotExistException(request.CustomerId);
            }

            var orderEntity = new Order
            {
                Description = request.Description,
                CreationDate = DateTime.Now,
                IsDistributed = true,
                CustomerId = request.CustomerId,
                SubcategoryId = request.SubcategoryId,
                Location = new Location
                {
                    PlaceId = request.PlaceId
                }
            };

            await _dbContext.Orders.AddAsync(orderEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.Base64Photos?.Any() ?? false)
            {
                _eventBus.Publish(new OrderWithPhotosAddedIntegrationEvent
                {
                    Base64Photos = request.Base64Photos,
                    OrderId = orderEntity.Id
                });
            }

            return Unit.Value;
        }
    }
}