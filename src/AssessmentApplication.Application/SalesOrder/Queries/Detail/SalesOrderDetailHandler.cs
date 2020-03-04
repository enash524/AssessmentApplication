using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.SalesOrder.Queries.Detail
{
    public class SalesOrderDetailHandler : IRequestHandler<SalesOrderDetailQuery, SalesOrderDetailEntity>
    {
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;

        public SalesOrderDetailHandler(ISalesOrderDetailRepository salesOrderDetailRepository)
        {
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        public Task<SalesOrderDetailEntity> Handle(SalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            return _salesOrderDetailRepository.GetSalesOrderDetailAsync(request.SalesOrderDetailId, cancellationToken);
        }
    }
}
