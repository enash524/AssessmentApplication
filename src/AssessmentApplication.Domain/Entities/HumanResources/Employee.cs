using System;

namespace AssessmentApplication.Domain.Entities.HumanResources.HumanResources
{
    public class Employee
    {
        public int BusinessEntityId { get; set; }

        public string NationalIdnumber { get; set; }

        public string LoginId { get; set; }

        public short? OrganizationLevel { get; set; }

        public string JobTitle { get; set; }

        public DateTime BirthDate { get; set; }

        public string MaritalStatus { get; set; }

        public string Gender { get; set; }

        public DateTime HireDate { get; set; }

        public bool? SalariedFlag { get; set; }

        public short VacationHours { get; set; }

        public short SickLeaveHours { get; set; }

        public bool? CurrentFlag { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
