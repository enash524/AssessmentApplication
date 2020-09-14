using System;
using System.Collections.Generic;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Queries.Sales.SalesOrderHeader
{
    /// <summary>
    /// GetSalesOrderHeaderQuery input parameters
    /// </summary>
    public class GetSalesOrderHeaderQuery : IRequest<QueryResult<PagedResponse<List<SalesOrderHeaderEntity>>>>
    {
        /// <summary>
        /// Order date start
        /// </summary>
        public DateTime? OrderDateStart { get; set; }

        /// <summary>
        /// Order date end
        /// </summary>
        public DateTime? OrderDateEnd { get; set; }

        /// <summary>
        /// Due date start
        /// </summary>
        public DateTime? DueDateStart { get; set; }

        /// <summary>
        /// Due date end
        /// </summary>
        public DateTime? DueDateEnd { get; set; }

        /// <summary>
        /// Ship date start
        /// </summary>
        public DateTime? ShipDateStart { get; set; }

        /// <summary>
        /// Ship date end
        /// </summary>
        public DateTime? ShipDateEnd { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        public string CustomerName { get; set; }
    }
}
