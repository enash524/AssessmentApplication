using System;

namespace AssessmentApplication.Domain.Entities
{
    public class SalesOrderHeaderEntity
    {
        public int SalesOrderId { get; set; }

        public int BusinessEntityId { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string AccountNumber { get; set; }

        public int AddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string StateProvinceCode { get; set; }

        public string PostalCode { get; set; }

        public string ShipMethodName { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string FullName { get; set; }
    }
}
