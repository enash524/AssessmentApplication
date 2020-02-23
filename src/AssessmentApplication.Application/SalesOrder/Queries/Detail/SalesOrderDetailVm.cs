using System;

namespace AssessmentApplication.Application.SalesOrder.Queries.Detail
{
    public class SalesOrderDetailVm
    {
        public decimal LineTotal { get; set; }
        
        public string Name { get; set; }

        public Int16 OrderQty { get; set; }

        public string ProductNumber { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }
    }
}
