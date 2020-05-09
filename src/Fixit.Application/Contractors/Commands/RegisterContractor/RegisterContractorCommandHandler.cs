using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Customers.Exceptions;
using Fixit.Domain.Entities;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Commands.RegisterContractor
{
  public class RegisterContractorCommandHandler : ICommandHandler<RegisterContractorCommand>
  {
    private readonly IFixitDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public RegisterContractorCommandHandler(IFixitDbContext dbContext, UserManager<User> userManager)
    {
      _dbContext = dbContext;
      _userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterContractorCommand request, CancellationToken cancellationToken)
    {
      if (await _dbContext.Contractors.AnyAsync(x => x.Email == request.Email, cancellationToken: cancellationToken))
      {
        throw new UserAlreadyExistsException(request.Email);
      }

      /// If there is already location with specified PlaceId, then we use it. In other case we create new Location.
      /// There is a difference between this and original project, as we get PlaceId from the forntend, not te foreign key to location.
      var location =
        await _dbContext.Locations.SingleOrDefaultAsync(x => x.PlaceId == request.PlaceId, cancellationToken: cancellationToken);
       
      if (location == null)
      {
        var response = await _dbContext.Locations.AddAsync(new Location
        {
          PlaceId = request.PlaceId,
        }, cancellationToken);

        location = response.Entity;
      }

      var contractor = new Contractor
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        CompanyName = request.CompanyName,
        PhoneNumber = request.PhoneNumber,
        LocationId = location.Id
      };

      contractor.UserName = contractor.Email;
      var roles = new List<string> { "Contractor" };

      var user = contractor;

      var result = await _userManager.CreateAsync(user, request.Password);

      if (!result.Succeeded)
      {
        throw new RegisteringUserException(request.Email);
      }

      result = await _userManager.AddToRolesAsync(user, roles);

      if (!result.Succeeded)
      {
        throw new RegisteringUserException(request.Email);
      }

      await _dbContext.SaveChangesAsync(cancellationToken);
      return Unit.Value;
    }
  }
}
