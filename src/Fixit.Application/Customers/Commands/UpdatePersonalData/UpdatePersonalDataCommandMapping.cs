using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Customers.Commands.UpdatePersonalData
{
    public class UpdatePersonalDataCommandMapping : Profile
    {
        public UpdatePersonalDataCommandMapping()
        {
            CreateMap<UpdatePersonalDataCommand, Customer>();
        }
    }
}