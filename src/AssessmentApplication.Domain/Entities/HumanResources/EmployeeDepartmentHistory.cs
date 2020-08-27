using System;

namespace AssessmentApplication.Domain.Entities.HumanResources.HumanResources
{
    public class EmployeeDepartmentHistory
    {
        public int BusinessEntityId { get; set; }

        public short DepartmentId { get; set; }

        public byte ShiftId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
