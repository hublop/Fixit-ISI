using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Common;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Queries.GetProfile
{
    public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, ContractorProfile>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProfileQueryHandler(IFixitDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ContractorProfile> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _dbContext.Contractors
                .Include(x => x.Location)
                .Include(x => x.RepairServices)
                .ThenInclude(x => x.SubCategory)
                .Include(x => x.RepairServices)
                .ThenInclude(x => x.SubCategory.Category)
                .Include(x => x.RepairServices)
                .ThenInclude(x => x.Opinions)
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == request.ContractorId, cancellationToken: cancellationToken);

            if (contractor == null)
            {
                throw new EntityNotFoundException(nameof(Contractor), request.ContractorId);
            }

            var mapped = _mapper.Map<ContractorProfile>(contractor);
            foreach (var categoryInProfile in mapped.Categories)
            {
                foreach (var subcategoryInProfile in categoryInProfile.Subcategories)
                {
                    subcategoryInProfile.Price = mapped.RepairServices
                        .First(x => x.SubCategory.Name == subcategoryInProfile.Name).Price;
                }
            }

            return mapped;
        }
    }
}