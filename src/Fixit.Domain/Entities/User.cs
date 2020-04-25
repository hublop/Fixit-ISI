using System;
using Microsoft.AspNetCore.Identity;

namespace Fixit.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        protected DateTime RegistrationDate { get; set; }
        public DateTime RefreshTokenCreatedDate { get; set; }
        public DateTime RefreshTokenExpiryDate { get; set; }
    }
}