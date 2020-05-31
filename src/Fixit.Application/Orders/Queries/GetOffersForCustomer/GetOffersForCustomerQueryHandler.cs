using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fixit.Application.Common.Interfaces;
using Fixit.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Orders.Queries.GetOffersForCustomer
{
    public class GetOffersForCustomerQueryHandler : IQueryHandler<GetOffersForCustomerQuery, List<OrderWithOffer>>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOffersForCustomerQueryHandler(IMapper mapper, IFixitDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public Task<List<OrderWithOffer>> Handle(GetOffersForCustomerQuery request, CancellationToken cancellationToken)
        {
          var query = _dbContext.Orders
            .Include(x => x.Location)
            .Include(x => x.Subcategory)
            .Include(x => x.Subcategory.Category)
            .Include(x => x.OrderImages)
            .Include(x => x.OrderImages)
            .ThenInclude(x => x.Image)
            .Include(x => x.OrderOffers)
            .ThenInclude(x => x.Contractor)
            .ThenInclude(x => x.Image)
            .AsQueryable();

            if (request.CustomerId.HasValue)
            {
              query = query.Where(x => x.CustomerId == request.CustomerId.Value);
            }

            if (request.OrderId.HasValue)
            {
                query = query.Where(x => x.Id == request.OrderId.Value);
            }

            return query
                .ProjectTo<OrderWithOffer>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}