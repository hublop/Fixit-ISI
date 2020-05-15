using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using Fixit.Shared.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Queries.GetList
{
    public class GetContractorsListQueryHandler : IQueryHandler<GetContractorsListQuery, PaginatedResult<ContractorForList>>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetContractorsListQueryHandler(IFixitDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ContractorForList>> Handle(GetContractorsListQuery request, CancellationToken cancellationToken)
        {
            var contractorsQuery = _dbContext.Contractors
                .Include(x => x.Location)
                .Include(x => x.RepairServices)
                .ThenInclude(x => x.Opinions)
                .Include(x => x.RepairServices)
                .ThenInclude(x => x.SubCategory)
                .Include(x => x.RepairServices)
                .ThenInclude(x => x.SubCategory.Category)
                .Include(x => x.Image)
                .Include(x => x.SubscriptionStatus)
                .AsNoTracking();

            if (request.ContractorsListFilter.SubcategoryId.HasValue)
            {
                contractorsQuery = contractorsQuery.Where(x =>
                    x.RepairServices.Any(y => y.SubCategoryId == request.ContractorsListFilter.SubcategoryId.Value));
            }

            if (!string.IsNullOrEmpty(request.ContractorsListFilter.PlaceId))
            {
                contractorsQuery = contractorsQuery.Where(x => x.Location.PlaceId == request.ContractorsListFilter.PlaceId);
            }

            if (!string.IsNullOrEmpty(request.ContractorsListFilter.NameSearchString))
            {
                var searchString = request.ContractorsListFilter.NameSearchString.ToLower();
                contractorsQuery = contractorsQuery.Where(x => x.FirstName.ToLower().Contains(searchString) ||
                                                               x.LastName.ToLower().Contains(searchString));
            }

            var paginatedEntities = new PaginatedResult<Contractor>(contractorsQuery,
                request.PagingParams.PageNumber, request.PagingParams.PageSize);

            return new PaginatedResult<ContractorForList>
            {
                PageNumber = paginatedEntities.PageNumber,
                PageSize = paginatedEntities.PageSize,
                TotalItems = paginatedEntities.TotalItems,
                TotalPages = paginatedEntities.TotalPages,
                Result = _mapper.Map<List<ContractorForList>>(paginatedEntities.Result)
            };
        }
    }
}