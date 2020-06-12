using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Fixit.Persistance.ExampleData
{
  public class CustomersSeed
  {

    private readonly IFixitDbContext _context;
    private readonly UserManager<User> _userManager;
    const string password = "P_ssw0rd";
    public CustomersSeed(IFixitDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task Seed()
    {
        var roles = new List<string> { "Customer" };
        try
        {
            if (!_context.Customers.Any())
            {
                foreach (var preconfiguredContractor in GetPreconfiguredCustomers())
                {
                    var result = await _userManager.CreateAsync(preconfiguredContractor, password);

                    result = await _userManager.AddToRolesAsync(preconfiguredContractor, roles);
                }
                await _context.SaveChangesAsync(new CancellationToken());
            }
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    private IEnumerable<Customer> GetPreconfiguredCustomers()
    {
        var customers = new List<Customer>()
        {
            new Customer
            {
                FirstName = "Mateusz",
                LastName = "Kowalonek",
                Email = "hub@gmail.com",
                UserName = "hub@gmail.com",
                PhoneNumber = "517213953"
            },
            new Customer
            {
                FirstName = "Hubert",
                LastName = "Nowakowski",
                Email = "hubertdruid@gmail.com",
                UserName = "hubertdruid@gmail.com",
                PhoneNumber = "111222333"
            },
            new Customer
            {
                FirstName = "Boleslaw",
                LastName = "Pierwszy",
                Email = "boleslaw1@gmail.com",
                UserName = "boleslaw1@gmail.com",
                PhoneNumber = "111222333"
            },
            new Customer
            {
                FirstName = "Mateusz",
                LastName = "Sztok",
                Email = "sztocki@gmail.com",
                UserName = "sztocki@gmail.com",
                PhoneNumber = "111222333"
            },
            new Customer
            {
                FirstName = "Adrian",
                LastName = "Ostatni",
                Email = "adi.ostatni@gmail.com",
                UserName = "adi.ostatni@gmail.com",
                PhoneNumber = "111222333"
            },
            new Customer
            {
                FirstName = "Marian",
                LastName = "Nowak",
                Email = "mariannowak123@gmail.com",
                UserName = "mariannowak123@gmail.com",
                PhoneNumber = "111222333"
            }
        };

        return customers;
    }
  }
}
