using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Exceptions;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Commands.AddRepairService
{
    public class AddRepairServiceCommandHandler : ICommandHandler<AddRepairServiceCommand>
    {
        private readonly IFixitDbContext _dbContext;

        public AddRepairServiceCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddRepairServiceCommand request, CancellationToken cancellationToken)
        {
            if (!await _dbContext.Subcategories.AnyAsync(x => x.Id == request.SubcategoryId, cancellationToken: cancellationToken))
            {
                throw new SubcategoryDoesNotExistException(request.SubcategoryId);
            }

            var contractor = await _dbContext.Contractors
                .Include(x => x.RepairServices)
                .FirstOrDefaultAsync(x => x.Id == request.ContractorId, cancellationToken);

            if (contractor == null)
            {
                throw new ContractorDoesNotExistException(request.ContractorId);
            }

            contractor.ProvideRepairService(request.SubcategoryId, 200);// not used anyway :)

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}