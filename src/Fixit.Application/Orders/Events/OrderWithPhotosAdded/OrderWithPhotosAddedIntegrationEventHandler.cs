using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Common.Services.Media;
using Fixit.Domain.Entities;
using Fixit.EventBus.Abstractions;

namespace Fixit.Application.Orders.Events.OrderWithPhotosAdded
{
    public class OrderWithPhotosAddedIntegrationEventHandler : IIntegrationEventHandler<OrderWithPhotosAddedIntegrationEvent>
    {
        private readonly IImageService _imageService;
        private readonly IFixitDbContext _dbContext;

        public OrderWithPhotosAddedIntegrationEventHandler(IImageService imageService, IFixitDbContext dbContext)
        {
            _imageService = imageService;
            _dbContext = dbContext;
        }

        public async Task Handle(OrderWithPhotosAddedIntegrationEvent @event)
        {
            var orderEntity = await _dbContext.Orders.FindAsync(@event.OrderId);
            if (orderEntity == null)
            {
                throw new BadRequestException($"Order does not exist: {@event.OrderId}");
            }

            foreach (var base64Photo in @event.Base64Photos)
            {
                var uploadResult = await _imageService.UploadAsync(new ImageUploadParameters
                {
                    ImageBas64Uri = base64Photo
                });

                await _dbContext.OrderImages.AddAsync(new OrderImage
                {
                    Image = new Image
                    {
                        Name = uploadResult.PublicId,
                        Url = uploadResult.Uri.ToString()
                    },
                    OrderId = @event.OrderId
                });
            }

            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}