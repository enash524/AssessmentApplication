using AssessmentApplication.Application.Sales.SalesOrderHeader.Queries;
using AssessmentApplication.Data.Requests;
using AutoMapper;

namespace AssessmentApplication.Application.Mappers
{
    public class SalesOrderHeaderProfile : Profile
    {
        public SalesOrderHeaderProfile()
        {
            CreateMap<GetSalesOrderHeaderQuery, SalesOrderHeaderRequest>();
        }
    }
}