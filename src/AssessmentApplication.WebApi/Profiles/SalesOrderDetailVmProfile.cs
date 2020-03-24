using AssessmentApplication.Domain.Entities;
using AssessmentApplication.WebApi.Models;
using AutoMapper;

namespace AssessmentApplication.WebApi.Profiles
{
    public class SalesOrderDetailVmProfile : Profile
    {
        public SalesOrderDetailVmProfile()
        {
            CreateMap<SalesOrderDetailEntity, SalesOrderDetailVm>();
        }
    }
}