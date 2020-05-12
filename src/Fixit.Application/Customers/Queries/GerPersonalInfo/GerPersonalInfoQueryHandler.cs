using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Common;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Customers.Queries.GerPersonalInfo
{
    public class GerPersonalInfoQueryHandler : IQueryHandler<GetPersonalInfoQuery, CustomerPersonalInfo>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public GerPersonalInfoQueryHandler(IFixitDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CustomerPersonalInfo> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

            if (customer == null)
            {
                throw new EntityNotFoundException(nameof(Customer), request.CustomerId);
            }

            return _mapper.Map<CustomerPersonalInfo>(customer);
        }
    }
}