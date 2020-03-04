using AssessmentApplication.Application.SalesOrder.Queries.Detail;
using AssessmentApplication.Domain.Entities;
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
