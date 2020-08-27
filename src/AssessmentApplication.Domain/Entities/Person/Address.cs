﻿using System;

namespace AssessmentApplication.Domain.Entities.Person.Person
{
    public class Address
    {
        public int AddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public int StateProvinceId { get; set; }

        public string PostalCode { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
