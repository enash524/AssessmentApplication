using AssessmentApplication.Data.Dto;
using AssessmentApplication.Domain.Entities;
using AutoMapper;

namespace AssessmentApplication.Data.Profiles
{
    public class SalesOrderDetailProfile : Profile
    {
        public SalesOrderDetailProfile()
        {
            CreateMap<SalesOrderDetailDto, SalesOrderDetailEntity>();
        }
    }
}