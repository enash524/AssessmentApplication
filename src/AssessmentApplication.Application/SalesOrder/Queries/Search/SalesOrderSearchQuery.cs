using System;
using System.Collections.Generic;
using MediatR;

namespace AssessmentApplication.Application.SalesOrder.Queries.Search
{
    public class SalesOrderSearchQuery : IRequest<List<SalesOrderSearchVm>>
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
