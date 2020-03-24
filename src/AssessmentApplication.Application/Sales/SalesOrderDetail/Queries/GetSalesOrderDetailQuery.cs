using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Sales.SalesOrderDetail.Queries
{
    public class GetSalesOrderDetailQuery : IRequest<SalesOrderDetailEntity>
    {
        public int SalesOrderDetailId { get; set; }
    }
}