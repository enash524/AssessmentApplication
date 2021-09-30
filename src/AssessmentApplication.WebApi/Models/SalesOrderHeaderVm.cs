namespace AssessmentApplication.WebApi.Models
{
    public class SalesOrderHeaderVm
    {
        public int SalesOrderId { get; set; }

        public string AccountNumber { get; set; }

        public decimal Freight { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal TotalDue { get; set; }

        public PersonVm Person { get; set; }

        public ShipMethodVm ShipMethod { get; set; }

        public AddressVm ShipToAddress { get; set; }
    }
}
