using MediatR;

namespace AssessmentApplication.Application.SalesOrder.Queries.Detail
{
    public class SalesOrderDetailQuery : IRequest<SalesOrderDetailVm>
    {
        public int SalesOrderDetailId { get; set; }
    }
}
