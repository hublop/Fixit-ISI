using AutoMapper;
using Fixit.Domain.Entities;

namespace Fixit.Application.Contractors.Commands.UpdatePersonalData
{
    public class UpdatePersonalDataCommandMapping : Profile
    {
        public UpdatePersonalDataCommandMapping()
        {
            CreateMap<UpdateContractorPersonalDataCommand, Contractor>();
        }
    }
}