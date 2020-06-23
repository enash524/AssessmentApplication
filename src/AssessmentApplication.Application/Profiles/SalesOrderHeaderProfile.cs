using AssessmentApplication.Application.Queries.Sales.SalesOrderHeader;
using AssessmentApplication.Data.Requests;
using AutoMapper;

namespace AssessmentApplication.Application.Mappers
{
    /// <summary>
    /// Provides a named configuration for maps. Naming conventions become scoped per profile.
    /// </summary>
    public class SalesOrderHeaderProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderHeaderProfile`1"/> class.
        /// </summary>
        public SalesOrderHeaderProfile()
        {
            CreateMap<GetSalesOrderHeaderQuery, SalesOrderHeaderRequest>();
        }
    }
}