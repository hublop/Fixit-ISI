using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Exceptions;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Commands.RemoveRepairService
{
    public class RemoveRepairServiceCommandHandler : ICommandHandler<RemoveRepairServiceCommand>
    {
        private readonly IFixitDbContext _dbContext;

        public RemoveRepairServiceCommandHandler(IFixitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(RemoveRepairServiceCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _dbContext.Contractors
                .Include(x => x.RepairServices)
                .FirstOrDefaultAsync(x => x.Id == request.ContractorId, cancellationToken);

            if (contractor == null)
            {
                throw new ContractorDoesNotExistException(request.ContractorId);
            }

            contractor.UnprovideRepairService(request.SubcategoryId);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}