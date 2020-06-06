using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Exceptions;
using Fixit.Application.Customers.Exceptions;
using Fixit.Application.Orders.Events.DirectOrderCreated;
using Fixit.Application.Orders.Events.OrderWithPhotosAdded;
using Fixit.Domain.Entities;
using Fixit.EventBus.Abstractions;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Orders.Commands.CreateDirectOrder
{
    public class CreateDirectOrderCommandHandler : ICommandHandler<CreateDirectOrderCommand>
    {
        private const double c_accuracy = 0.00001;
    private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public CreateDirectOrderCommandHandler(IFixitDbContext dbContext, IMapper mapper, IEventBus eventBus)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateDirectOrderCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _dbContext.Contractors.FindAsync(request.ContractorId);
            if (contractor == null)
            {
                throw new ContractorDoesNotExistException(request.ContractorId);
            }

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
                ContractorId = request.ContractorId,
                CreationDate = DateTime.Now,
                IsDistributed = false,
                CustomerId = request.CustomerId,
                SubcategoryId = request.SubcategoryId,
                Location = new Location()
                {
                    PlaceId = request.PlaceId,
                    Longitude = request.Longitude,
                    Latitude = request.Latitude
                }
            };

            await _dbContext.Orders.AddAsync(orderEntity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.Base64Photos.Any())
            {
                _eventBus.Publish(new OrderWithPhotosAddedIntegrationEvent
                {
                    Base64Photos = request.Base64Photos,
                    OrderId = orderEntity.Id
                });
            }

            _eventBus.Publish(new DirectOrderCreatedIntegrationEvent {OrderId = orderEntity.Id});

            return Unit.Value;
        }
    }
}