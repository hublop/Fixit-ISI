using System.Collections.Generic;

namespace Fixit.Application.Common.Services.Identity
{
    public class JsonWebTokenUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
    }
}