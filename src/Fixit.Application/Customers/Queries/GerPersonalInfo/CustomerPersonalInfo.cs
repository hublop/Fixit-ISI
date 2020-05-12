using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Customers.Queries.GerPersonalInfo
{
    public class CustomerPersonalInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class GetPersonalInfoQueryMapping : Profile
    {
        public GetPersonalInfoQueryMapping()
        {
            CreateMap<Customer, CustomerPersonalInfo>();
        }
    }
}