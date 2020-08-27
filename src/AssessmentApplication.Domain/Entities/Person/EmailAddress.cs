using System;

namespace AssessmentApplication.Domain.Entities.Person.Person
{
    public class EmailAddress
    {
        public int BusinessEntityId { get; set; }

        public int EmailAddressId { get; set; }

        public string EmailAddress1 { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
