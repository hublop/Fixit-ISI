using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;

namespace Fixit.Persistance.ExampleData
{
  public class LocationsSeed
  {
      private readonly IFixitDbContext _context;

      public LocationsSeed(IFixitDbContext context)
      {
          _context = context;
      }

      public async Task Seed()
    {
        try
        {
            if (!_context.Locations.Any())
            {
                _context.Locations.AddRange(GetPreconfiguredLocations());
                await _context.SaveChangesAsync(new CancellationToken());
            }
        }
        catch (Exception e)
        {
            
        }
    }

      private IEnumerable<Location> GetPreconfiguredLocations()
      {
          return new List<Location>
          {
              new Location
              {
                  PlaceId = "ChIJ3f0CnIjCD0cRQB91xIzugF8",
                  Latitude = 51.097130,
                  Longitude = 17.056370
              },
              new Location
              {
                  PlaceId = "iFQcnVzYSAyMCwgNTAtMzE5IFdyb2PFgmF3LCBQb2xhbmQiMBIuChQKEgn3DSEX2-kPRxEg-UO97j48aBAUKhQKEgkfmu38z-kPRxGTraLc9sudGg",
                  Latitude = 51.119419,
                  Longitude = 17.043230
              },
              new Location
              {
                  PlaceId = "ChIJLyo4-DmYBUcRUcftvj_6Yk4",
                  Latitude = 51.838430,
                  Longitude = 16.579950
              }
          };
      }
  }
}
