using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Exceptions;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Commands.UpdatePersonalData
{
    public class UpdatePersonalDataCommandHandler : ICommandHandler<UpdateContractorPersonalDataCommand>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdatePersonalDataCommandHandler(IFixitDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContractorPersonalDataCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _dbContext.Contractors
                .Include(x => x.Location)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (contractor == null)
            {
                throw new ContractorDoesNotExistException(request.Id);
            }

            _mapper.Map(request, contractor);

            if (contractor.Location != null)
            {
                contractor.Location.PlaceId = request.PlaceId;
            }
            else
            {
                contractor.Location = new Location();
                contractor.Location.PlaceId = request.PlaceId;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}