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

namespace Fixit.Application.Customers.Commands.RegisterCustomer
{
  public class RegisterCustomerCommandHandler: ICommandHandler<RegisterCustomerCommand>
  {
    private readonly IFixitDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public RegisterCustomerCommandHandler(IFixitDbContext dbContext, UserManager<User> userManager)
    {
      _dbContext = dbContext;
      _userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
      if (await _dbContext.Customers.AnyAsync(x => x.Email == request.Email, cancellationToken: cancellationToken))
      {
        throw new UserAlreadyExistsException(request.Email);
      }

      var customer = new Customer
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        PhoneNumber = request.PhoneNumber
      };
      customer.UserName = customer.Email;

      var roles = new List<string> {"Customer"};

      var user = customer;

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
