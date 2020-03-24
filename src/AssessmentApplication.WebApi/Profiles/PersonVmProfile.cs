using AssessmentApplication.Domain.Entities;
using AssessmentApplication.WebApi.Models;
using AutoMapper;

namespace AssessmentApplication.WebApi.Profiles
{
    public class PersonVmProfile : Profile
    {
        public PersonVmProfile()
        {
            CreateMap<PersonEntity, PersonVm>();
        }
    }
}