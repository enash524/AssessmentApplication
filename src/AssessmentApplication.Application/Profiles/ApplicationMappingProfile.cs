using AssessmentApplication.Application.Queries.Sales.SalesOrderHeader;
using AssessmentApplication.Data.Requests;
using AutoMapper;

namespace AssessmentApplication.Application.Mappers
{
    /// <summary>
    /// Provides a named configuration for maps. Naming conventions become scoped per profile.
    /// </summary>
    public class ApplicationMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationMappingProfile`1"/> class.
        /// </summary>
        public ApplicationMappingProfile()
        {
            CreateMap<GetSalesOrderHeaderQuery, SalesOrderHeaderRequest>();
        }
    }
}
