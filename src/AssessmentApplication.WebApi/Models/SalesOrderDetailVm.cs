using System;

namespace AssessmentApplication.WebApi.Models
{
    public class SalesOrderDetailVm
    {
        public decimal LineTotal { get; set; }
        
        public string Name { get; set; }

        public short OrderQty { get; set; }

        public string ProductNumber { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }
    }
}
