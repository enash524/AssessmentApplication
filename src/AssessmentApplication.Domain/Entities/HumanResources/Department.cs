using System;

namespace AssessmentApplication.Domain.Entities.HumanResources.HumanResources
{
    public class Department
    {
        public short DepartmentId { get; set; }

        public string Name { get; set; }

        public string GroupName { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
