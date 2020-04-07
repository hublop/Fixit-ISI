using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Common;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommandHandler : ICommandHandler<RemoveCategoryCommand>
    {
        private readonly IFixitDbContext _dbContext;
        public RemoveCategoryCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(RemoveCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

            if (category == null)
            {
                throw new DomainException("Requested category does not exist.",
                    $"Category with id {command.Id} cannot be removed because it does not exist.");
            }

            _dbContext.Categories.Remove(category);

            if (!await _dbContext.SaveChangesAsync(cancellationToken))
            {
                throw new DeleteFailureException(nameof(Category), command.Id);
            }

            return Unit.Value;
        }
    }
}