using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fixit.Application.Common.Interfaces;
using Fixit.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IList<CategoryInfoForList>>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IFixitDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<CategoryInfoForList>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
             return await _dbContext.Categories
                .Include(x => x.SubCategories)
                .OrderBy(x => x.Name)
                .ProjectTo<CategoryInfoForList>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}