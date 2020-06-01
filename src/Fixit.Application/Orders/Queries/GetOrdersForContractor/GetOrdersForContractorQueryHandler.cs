using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Orders.Queries.GetOrdersForContractor
{
  public class GetOrdersForContractorQueryHandler : IQueryHandler<GetOrdersForContractorQuery, List<OrderForContractor>>
  {
    private readonly IFixitDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrdersForContractorQueryHandler(IMapper mapper, IFixitDbContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
    }

    public async Task<List<OrderForContractor>> Handle(GetOrdersForContractorQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Orders
            .Include(x => x.DistributedOrders)
            .Where(x => x.ContractorId == request.ContractorId || x.DistributedOrders.Any(y => y.ContractorId == request.ContractorId));


        var ordersForContractor = await query
            .ProjectTo<OrderForContractor>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var orderOffers = await _dbContext.OrderOffers
            .Where(x => x.ContractorId == request.ContractorId)
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var orderForContractor in ordersForContractor)
        {
            orderForContractor.Status = orderOffers.Any(x =>
                x.ContractorId == request.ContractorId && x.OrderId == orderForContractor.Id);
        }

        return ordersForContractor;
    }
  }
}
