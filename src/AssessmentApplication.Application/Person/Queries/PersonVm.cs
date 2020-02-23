﻿using System;

namespace AssessmentApplication.Application.Person.Queries
{
    public class PersonVm
    {
        public int BusinessEntityId { get; set; }

        public string PersonType { get; set; }

        public bool NameStyle { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public int EmailPromotion { get; set; }

        public string AdditionalContactInfo { get; set; }

        public string Demographics { get; set; }

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
