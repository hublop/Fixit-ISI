using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Fixit.Persistance.ExampleData
{
    public class GlobalSeed
    {
        private readonly IFixitDbContext _context;

        private readonly UserManager<User> _userManager;


    public GlobalSeed(IFixitDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

        public async Task Seed()
        {
            CategoriesSeed cs = new CategoriesSeed(_context);
            await cs.Seed();

            LocationsSeed ls = new LocationsSeed(_context);
            await ls.Seed();

            ContractorsSeed contractorsSeed = new ContractorsSeed(_context, _userManager);
            await contractorsSeed.Seed();

            CustomersSeed customersSeed = new CustomersSeed(_context, _userManager);
            await customersSeed.Seed();
    }
    }
}
