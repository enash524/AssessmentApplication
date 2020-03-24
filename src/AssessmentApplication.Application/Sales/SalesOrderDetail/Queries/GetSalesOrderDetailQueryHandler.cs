using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using MediatR;

namespace AssessmentApplication.Application.Sales.SalesOrderDetail.Queries
{
    public class GetSalesOrderDetailQueryHandler : IRequestHandler<GetSalesOrderDetailQuery, SalesOrderDetailEntity>
    {
        private ISalesRepository _salesOrderDetailRepository;

        public GetSalesOrderDetailQueryHandler(ISalesRepository salesOrderDetailRepository)
        {
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        public Task<SalesOrderDetailEntity> Handle(GetSalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            return _salesOrderDetailRepository.GetSalesOrderDetailAsync(request.SalesOrderDetailId, cancellationToken);
        }
    }
}