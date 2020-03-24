using System;
using System.Collections.Generic;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Sales.SalesOrderHeader.Queries
{
    public class GetSalesOrderHeaderQuery : IRequest<PagedResponse<List<SalesOrderHeaderEntity>>>
    {
        public DateTime? OrderDateStart { get; set; }

        public DateTime? OrderDateEnd { get; set; }

        public DateTime? DueDateStart { get; set; }

        public DateTime? DueDateEnd { get; set; }

        public DateTime? ShipDateStart { get; set; }

        public DateTime? ShipDateEnd { get; set; }

        public string CustomerName { get; set; }
    }
}