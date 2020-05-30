using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fixit.Application.Orders.Queries.GetOrdersForContractor
{
  public class GetOrdersForContractorQueryHandler : IQueryHandler<GetOrdersForContractorQuery, List<OrderOfferForContractor>>
  {
    private readonly IFixitDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrdersForContractorQueryHandler(IMapper mapper, IFixitDbContext dbContext)
    {
      _mapper = mapper;
      _dbContext = dbContext;
    }

    public Task<List<OrderOfferForContractor>> Handle(GetOrdersForContractorQuery request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
