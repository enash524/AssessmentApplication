using System;

namespace AssessmentApplication.Data.Requests
{
    public class SalesOrderHeaderRequest : PagedRequest
    {
        public SalesOrderHeaderRequest()
        {
            SortBy = "SalesOrderID";
        }

        public DateTime? OrderDateStart { get; set; }

        public DateTime? OrderDateEnd { get; set; }

        public DateTime? DueDateStart { get; set; }

        public DateTime? DueDateEnd { get; set; }

        public DateTime? ShipDateStart { get; set; }

        public DateTime? ShipDateEnd { get; set; }

        public string CustomerName { get; set; }
    }
}