using AssessmentApplication.Data.Dto;
using AssessmentApplication.Domain.Entities;
using AutoMapper;

namespace AssessmentApplication.Data.Profiles
{
    /// <summary>
    /// Provides a named configuration for maps. Naming conventions become scoped per profile.
    /// </summary>
    public class DataMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMappingProfile`1"/> class.
        /// </summary>
        public DataMappingProfile()
        {
            CreateMap<PersonDto, PersonEntity>();
            CreateMap<SalesOrderDetailDto, SalesOrderDetailEntity>();
            CreateMap<SalesOrderHeaderDto, SalesOrderHeaderEntity>();
        }
    }
}
