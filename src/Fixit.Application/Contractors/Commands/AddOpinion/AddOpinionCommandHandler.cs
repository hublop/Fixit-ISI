using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Common.Services.Mail;
using Fixit.Application.Contractors.Exceptions;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Commands.AddOpinion
{
    public class AddOpinionCommandHandler : ICommandHandler<AddOpinionCommand>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMailService _mailService;

        public AddOpinionCommandHandler(IFixitDbContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }


        public async Task<Unit> Handle(AddOpinionCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _dbContext.Contractors
                .Include(x => x.RepairServices)
                .FirstOrDefaultAsync(x => x.Id == request.ContractorId, cancellationToken);

            if (contractor == null)
            {
                throw new ContractorDoesNotExistException(request.ContractorId);
            }

            if (!await _dbContext.Subcategories.AnyAsync(x => x.Id == request.SubcategoryId, cancellationToken))
            {
                throw new SubcategoryDoesNotExistException(request.SubcategoryId);
            }

            contractor.AddOpinion(request.Comment, request.SubcategoryId, request.Punctuality,
                request.Involvement, request.Quality);

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _mailService.SendEmailAsync(contractor.Email, "New Opinion",
                "You have new opinion: " + request.Comment);

            return Unit.Value;
        }
    }
}