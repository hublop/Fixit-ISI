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
    public class GetOffersForCustomerQueryHandler : IQueryHandler<GetOffersForCustomerQuery, List<OrderOfferForCustomer>>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOffersForCustomerQueryHandler(IMapper mapper, IFixitDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public Task<List<OrderOfferForCustomer>> Handle(GetOffersForCustomerQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.OrderOffers
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Image)
                .Include(x => x.Order)
                .ThenInclude(x => x.Location)
                .Include(x => x.Order)
                .ThenInclude(x => x.Subcategory)
                .Include(x => x.Order)
                .ThenInclude(x => x.Subcategory.Category)
                .Include(x => x.Order)
                .ThenInclude(x => x.OrderImages)
                .Include(x => x.Order)
                .ThenInclude(x => x.OrderImages)
                .ThenInclude(x => x.Image)
                .Where(x => x.Order.CustomerId == request.CustomerId);

            if (request.OrderId.HasValue)
            {
                query = query.Where(x => x.OrderId == request.OrderId.Value);
            }

            return query
                .ProjectTo<OrderOfferForCustomer>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}