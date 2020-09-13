using System;

namespace AssessmentApplication.Models.SalesOrder
{
    public class SalesOrderSearchModel
    {
        public string CustomerName { get; set; }

        public DateTime? DueDateEnd { get; set; }

        public DateTime? DueDateStart { get; set; }

        public DateTime? OrderDateEnd { get; set; }

        public DateTime? OrderDateStart { get; set; }

        public DateTime? ShipDateEnd { get; set; }

        public DateTime? ShipDateStart { get; set; }
    }
}
