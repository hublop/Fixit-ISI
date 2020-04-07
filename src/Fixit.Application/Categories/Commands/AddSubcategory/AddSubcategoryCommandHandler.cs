using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Categories.Exceptions;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Categories.Commands.AddSubcategory
{
    public class AddSubcategoryCommandHandler : ICommandHandler<AddSubcategoryCommand, int>
    {
        private readonly IFixitDbContext _dbContext;

        public AddSubcategoryCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .Include(x => x.SubCategories)
                .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken: cancellationToken);

            if (category == null)
            {
                throw new CategoryDoesNotExistException(request.CategoryId);
            }

            var subCategory = new Subcategory(request.Name, request.Description);
            category.AddSubCategory(subCategory);

            _dbContext.Categories.Update(category);

            if (await _dbContext.SaveChangesAsync(cancellationToken))
            {
                return subCategory.Id;
            }

            throw new UpdateFailureException(nameof(Category), request.CategoryId);
        }
    }
}