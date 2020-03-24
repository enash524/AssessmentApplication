using System;

namespace AssessmentApplication.Domain.Entities.Person.Person
{
    public class BusinessEntity
    {
        public int BusinessEntityId { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}