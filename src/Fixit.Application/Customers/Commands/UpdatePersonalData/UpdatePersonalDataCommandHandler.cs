using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Customers.Exceptions;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Customers.Commands.UpdatePersonalData
{
    public class UpdatePersonalDataCommandHandler : ICommandHandler<UpdateCustomerPersonalDataCommand>
    {
        private readonly IFixitDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdatePersonalDataCommandHandler(IFixitDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCustomerPersonalDataCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (customer == null)
            {
                throw new CustomerDoesNotExistException(request.Id);
            }

            _mapper.Map(request, customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}