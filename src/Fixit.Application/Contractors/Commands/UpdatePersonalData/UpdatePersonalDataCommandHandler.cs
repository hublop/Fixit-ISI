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

            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.PlaceId == request.PlaceId, cancellationToken: cancellationToken);

            if (location == null)
            {
                location = new Location()
                {
                    PlaceId = request.PlaceId
                };
            }

            contractor.Location = location;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}