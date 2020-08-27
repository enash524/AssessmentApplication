using System;

namespace AssessmentApplication.Domain.Entities.Person.Person
{
    public class BusinessEntityAddress
    {
        public int BusinessEntityId { get; set; }

        public int AddressId { get; set; }

        public int AddressTypeId { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
