using System;

namespace AssessmentApplication.Domain.Entities.HumanResources.HumanResources
{
    public class Shift
    {
        public byte ShiftId { get; set; }

        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}