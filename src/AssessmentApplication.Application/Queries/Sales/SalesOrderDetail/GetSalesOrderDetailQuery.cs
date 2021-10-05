using System.Collections.Generic;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Queries.Sales.SalesOrderDetail
{
    /// <summary>
    /// GetSalesOrderDetail query input parameters
    /// </summary>
    public class GetSalesOrderDetailQuery : IRequest<QueryResult<List<SalesOrderDetailEntity>>>
    {
        /// <summary>
        /// The SalesOrderDetailId to query
        /// </summary>
        public int SalesOrderDetailId { get; set; }
    }
}
