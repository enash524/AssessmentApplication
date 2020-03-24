using System;

namespace AssessmentApplication.Domain.Entities.HumanResources.HumanResources
{
    public class JobCandidate
    {
        public int JobCandidateId { get; set; }

        public int? BusinessEntityId { get; set; }

        public string Resume { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}