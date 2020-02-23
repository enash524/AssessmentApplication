namespace AssessmentApplication.Application.SalesOrder.Queries.Search
{
    public class SalesOrderSearchVm
    {
        public string AccountNumber { get; set; }

        public decimal Freight { get; set; }

        //public Person Person { get; set; }

        public int SalesOrderId { get; set; }

        //public ShipMethod ShipMethod { get; set; }

        //public Address ShipToAddress { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal TotalDue { get; set; }
    }
}
