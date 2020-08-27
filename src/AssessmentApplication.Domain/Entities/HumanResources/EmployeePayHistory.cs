﻿using System;

namespace AssessmentApplication.Domain.Entities.HumanResources.HumanResources
{
    public class EmployeePayHistory
    {
        public int BusinessEntityId { get; set; }

        public DateTime RateChangeDate { get; set; }

        public decimal Rate { get; set; }

        public byte PayFrequency { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
