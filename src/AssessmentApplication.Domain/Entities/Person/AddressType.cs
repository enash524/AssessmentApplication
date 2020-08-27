using System;

namespace AssessmentApplication.Domain.Entities.Person.Person
{
    public class AddressType
    {
        public int AddressTypeId { get; set; }

        public string Name { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
