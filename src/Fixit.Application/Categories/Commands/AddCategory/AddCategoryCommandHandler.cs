using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Categories.Exceptions;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand, int>
    {
        private readonly IFixitDbContext _dbContext;

        public AddCategoryCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
        {
            if (await _dbContext.Categories.AnyAsync(x => x.Name == command.Name, cancellationToken: cancellationToken))
            {
                throw new CategoryAlreadyExistsException(command.Name);
            }

            var category = new Category(command.Name, command.Description);

            if (command.Subcategories != null && command.Subcategories.Any())
            {
                foreach (var subcategory in command.Subcategories)
                {
                    category.AddSubCategory(new Subcategory(subcategory.Name, subcategory.Description));
                }
            }

            await _dbContext.Categories.AddAsync(category, cancellationToken);

            if (await _dbContext.SaveChangesAsync(cancellationToken))
            {
                return category.Id;
            }

            throw new AddFailureException(nameof(Category));
        }
    }
}