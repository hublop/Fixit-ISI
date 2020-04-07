using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Common;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, CategoryInfo>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IMapper mapper, IFixitDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<CategoryInfo> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .Include(x => x.SubCategories)
                .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), query.Id);
            }

            return _mapper.Map<CategoryInfo>(category);
        }
    }
}