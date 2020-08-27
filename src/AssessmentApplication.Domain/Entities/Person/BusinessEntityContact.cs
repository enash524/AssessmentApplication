using System;

namespace AssessmentApplication.Domain.Entities.Person.Person
{
    public class BusinessEntityContact
    {
        public int BusinessEntityId { get; set; }

        public int PersonId { get; set; }

        public int ContactTypeId { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
