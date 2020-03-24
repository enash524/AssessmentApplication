using AssessmentApplication.Data.Dto;
using AssessmentApplication.Domain.Entities;
using AutoMapper;

namespace AssessmentApplication.Data.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDto, PersonEntity>();
        }
    }
}