using AssessmentApplication.Application.Queries.Sales.SalesOrderHeader;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AssessmentApplication.Models.SalesOrder;
using AssessmentApplication.WebApi.Models;
using AutoMapper;

namespace AssessmentApplication.WebApi.Profiles
{
    /// <summary>
    /// Provides a named configuration for maps. Naming conventions become scoped per profile.
    /// </summary>
    public class ApiMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiMappingProfile`1"/> class.
        /// </summary>
        public ApiMappingProfile()
        {
            CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
            CreateMap<PersonEntity, PersonVm>();
            CreateMap<SalesOrderDetailEntity, SalesOrderDetailVm>();
            CreateMap<SalesOrderHeaderEntity, SalesOrderHeaderVm>();
            CreateMap<SalesOrderSearchModel, GetSalesOrderHeaderQuery>();
        }
    }
}
