using System;

namespace AssessmentApplication.Domain.Entities.Person.Person
{
    public class StateProvince
    {
        public int StateProvinceId { get; set; }

        public string StateProvinceCode { get; set; }

        public string CountryRegionCode { get; set; }

        public bool? IsOnlyStateProvinceFlag { get; set; }

        public string Name { get; set; }

        public int TerritoryId { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
