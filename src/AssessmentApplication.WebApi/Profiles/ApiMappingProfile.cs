using System.Collections.Generic;
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
            CreateMap<SalesOrderSearchModel, GetSalesOrderHeaderQuery>();
            CreateMap<SalesOrderHeaderEntity, SalesOrderHeaderVm>()
                .ForMember(dest => dest.Person,
                    opts => opts.MapFrom(
                        src => new PersonVm
                        {
                            BusinessEntityId = src.BusinessEntityId,
                            Title = src.Title,
                            FirstName = src.FirstName,
                            MiddleName = src.MiddleName,
                            LastName = src.LastName,
                            Suffix = src.Suffix,
                            FullName = src.FullName
                        }))
                .ForMember(dest => dest.ShipMethod,
                    opts => opts.MapFrom(
                        src => new ShipMethodVm
                        {
                            ShipMethodId = src.ShipMethodId,
                            ShipMethodName = src.ShipMethodName
                        }))
                .ForMember(dest => dest.ShipToAddress,
                    opts => opts.MapFrom(
                        src => new AddressVm
                        {
                            AddressId = src.AddressId,
                            Address1 = src.AddressLine1,
                            Address2 = src.AddressLine2,
                            City = src.City,
                            StateOrProvinceCode = src.StateProvinceCode,
                            PostalCode = src.PostalCode
                        }));
        }
    }
}
