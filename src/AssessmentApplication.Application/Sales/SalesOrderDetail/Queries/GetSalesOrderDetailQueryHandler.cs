using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Application.Sales.SalesOrderDetail.Queries
{
    public class GetSalesOrderDetailQueryHandler : IRequestHandler<GetSalesOrderDetailQuery, SalesOrderDetailEntity>
    {
        private ILogger<GetSalesOrderDetailQueryHandler> _logger;
        private ISalesRepository _salesOrderDetailRepository;

        public GetSalesOrderDetailQueryHandler(ILogger<GetSalesOrderDetailQueryHandler> logger, ISalesRepository salesOrderDetailRepository)
        {
            _logger = logger;
            _salesOrderDetailRepository = salesOrderDetailRepository;
        }

        public Task<SalesOrderDetailEntity> Handle(GetSalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            return _salesOrderDetailRepository.GetSalesOrderDetailAsync(request.SalesOrderDetailId, cancellationToken);
        }
    }
}