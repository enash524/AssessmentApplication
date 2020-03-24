using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Requests;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;

namespace AssessmentApplication.Data.Interfaces
{
    public interface ISalesRepository
    {
        Task<SalesOrderDetailEntity> GetSalesOrderDetailAsync(int salesOrderId, CancellationToken cancellationToke);

        Task<PagedResponse<List<SalesOrderHeaderEntity>>> GetSalesOrderHeaderAsync(SalesOrderHeaderRequest request, CancellationToken cancellationToken);
    }
}