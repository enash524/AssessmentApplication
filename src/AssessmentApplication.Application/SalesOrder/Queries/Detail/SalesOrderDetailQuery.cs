using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.SalesOrder.Queries.Detail
{
    public class SalesOrderDetailQuery : IRequest<SalesOrderDetailEntity>
    {
        public int SalesOrderDetailId { get; set; }
    }
}
