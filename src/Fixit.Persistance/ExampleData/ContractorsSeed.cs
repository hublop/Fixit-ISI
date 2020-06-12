using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Customers.Exceptions;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Persistance.ExampleData
{
  public class ContractorsSeed
  {
      private readonly IFixitDbContext _context;
      private readonly UserManager<User> _userManager;
      const string password = "P_ssw0rd";
    public ContractorsSeed(IFixitDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task Seed()
    {
        var roles = new List<string> { "Contractor" };
      try
      {
        if (!_context.Contractors.Any())
        {
            foreach (var preconfiguredContractor in GetPreconfiguredContractors())
            {
                var result = await _userManager.CreateAsync(preconfiguredContractor, password);

                if (!result.Succeeded)
                {
                    throw new RegisteringUserException(preconfiguredContractor.Email);
                }

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

    private IEnumerable<Contractor> GetPreconfiguredContractors()
    {
        var contractors = new List<Contractor>()
        {
            new Contractor
            {
                CompanyName = "Janko Naprawiacz",
                FirstName = "Natalia",
                LastName = "Woźniak",
                UserName = "wozniczek.kowalski@gmail.com",
                Email = "wozniczek.kowalski@gmail.com",
                PhoneNumber = "111222333",
                SelfDescription =
                    "Some text written by Cicero that\'s used to fill spaces on graphical design and publishing before the actual words have been written. The reason why it is difficult to understand is to draw attention away from the words on a page and to focus it on the design instead.",
                ContractorFrom = DateTime.Now,
                LocationId = _context.Locations.First(x => x.PlaceId == "ChIJ3f0CnIjCD0cRQB91xIzugF8").Id,
                SubscriptionStatusId = _context.SubscriptionStatuses.First(x => x.Status == "Active").Id
            },
            new Contractor
            {
                CompanyName = "Janko Naprawiacz",
                FirstName = "Konrad",
                LastName = "Kowalski",
                UserName = "konrad.kowalski@gmail.com",
                Email = "konrad.kowalski@gmail.com",
                PhoneNumber = "111222333",
                SelfDescription =
                    "Some text written by Cicero that\'s used to fill spaces on graphical design and publishing before the actual words have been written. The reason why it is difficult to understand is to draw attention away from the words on a page and to focus it on the design instead.",
                ContractorFrom = DateTime.Now,
                LocationId = _context.Locations.First(x => x.PlaceId == "ChIJ3f0CnIjCD0cRQB91xIzugF8").Id,
                SubscriptionStatusId = _context.SubscriptionStatuses.First(x => x.Status == "Cancelled").Id
            },
            new Contractor
            {
                CompanyName = "Janko Naprawiacz",
                FirstName = "Jan",
                LastName = "Kowalski",
                UserName = "jan.kowalski@gmail.com",
                Email = "jan.kowalski@gmail.com",
                PhoneNumber = "111222333",
                SelfDescription =
                    "Some text written by Cicero that\'s used to fill spaces on graphical design and publishing before the actual words have been written. The reason why it is difficult to understand is to draw attention away from the words on a page and to focus it on the design instead.",
                ContractorFrom = DateTime.Now,
                LocationId = _context.Locations.First(x => x.PlaceId == "ChIJLyo4-DmYBUcRUcftvj_6Yk4").Id,
                SubscriptionStatusId = _context.SubscriptionStatuses.First(x => x.Status == "Active").Id
            },
            new Contractor
            {
                CompanyName = "Anna Korpo",
                FirstName = "Anna",
                LastName = "Kowalski",
                UserName = "anna.kowalski@gmail.com",
                Email = "anna.kowalski@gmail.com",
                PhoneNumber = "111222333",
                SelfDescription =
                    "Some text written by Cicero that\'s used to fill spaces on graphical design and publishing before the actual words have been written. The reason why it is difficult to understand is to draw attention away from the words on a page and to focus it on the design instead.",
                ContractorFrom = DateTime.Now,
                LocationId = _context.Locations.First(x => x.PlaceId == "ChIJLyo4-DmYBUcRUcftvj_6Yk4").Id,
                SubscriptionStatusId = _context.SubscriptionStatuses.First(x => x.Status == "Cancelled").Id
            },
            new Contractor
            {
                CompanyName = "Janko Naprawiacz",
                FirstName = "Jan",
                LastName = "Nowicki",
                UserName = "mateusz.kowalski@gmail.com",
                Email = "mateusz.kowalski@gmail.com",
                PhoneNumber = "111222333",
                SelfDescription =
                    "Some text written by Cicero that\'s used to fill spaces on graphical design and publishing before the actual words have been written. The reason why it is difficult to understand is to draw attention away from the words on a page and to focus it on the design instead.",
                ContractorFrom = DateTime.Now,
                LocationId = _context.Locations.First(x => x.PlaceId == "iFQcnVzYSAyMCwgNTAtMzE5IFdyb2PFgmF3LCBQb2xhbmQiMBIuChQKEgn3DSEX2-kPRxEg-UO97j48aBAUKhQKEgkfmu38z-kPRxGTraLc9sudGg").Id,
                SubscriptionStatusId = _context.SubscriptionStatuses.First(x => x.Status == "Active").Id
            },
            new Contractor
            {
                CompanyName = "Janko Naprawiacz",
                FirstName = "Kazimierz",
                LastName = "Kowalski",
                UserName =  "kazimierz.kowalski@gmail.com",
                Email = "kazimierz.kowalski@gmail.com",
                PhoneNumber = "111222333",
                SelfDescription =
                    "Some text written by Cicero that\'s used to fill spaces on graphical design and publishing before the actual words have been written. The reason why it is difficult to understand is to draw attention away from the words on a page and to focus it on the design instead.",
                ContractorFrom = DateTime.Now,
                LocationId = _context.Locations.First(x => x.PlaceId == "iFQcnVzYSAyMCwgNTAtMzE5IFdyb2PFgmF3LCBQb2xhbmQiMBIuChQKEgn3DSEX2-kPRxEg-UO97j48aBAUKhQKEgkfmu38z-kPRxGTraLc9sudGg").Id,
                SubscriptionStatusId = _context.SubscriptionStatuses.First(x => x.Status == "Cancelled").Id
            },
            new Contractor
            {
                CompanyName = "Janko Naprawiacz",
                FirstName = "Marian",
                LastName = "Chochołowski",
                UserName =  "chocholowski.kowalski@gmail.com",
                Email = "chocholowski.kowalski@gmail.com",
                PhoneNumber = "111222333",
                SelfDescription =
                    "Some text written by Cicero that\'s used to fill spaces on graphical design and publishing before the actual words have been written. The reason why it is difficult to understand is to draw attention away from the words on a page and to focus it on the design instead.",
                ContractorFrom = DateTime.Now,
                LocationId = _context.Locations.First(x => x.PlaceId == "ChIJLyo4-DmYBUcRUcftvj_6Yk4").Id,
                SubscriptionStatusId = _context.SubscriptionStatuses.First(x => x.Status == "Cancelled").Id
            },
           
        };

        for (int i = 0; i < 7; i++)
        {
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa bezpieczników").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa silników elektrycznych").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Udrażnianie zatkanych rur").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa kabin prysznicowych").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Diagnostyka komputerowa").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Wymiana/naprawa opon").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Przegląd sezonowy").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa zmywarki").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa lodówki").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa pralki").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa odkurzacza").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa zamrażarki").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa paneli").Id, 100);
            contractors[i].ProvideRepairService(_context.Subcategories.First(x => x.Name == "Naprawa dachu").Id, 100);
        }

        return contractors;
    }
  }
}
