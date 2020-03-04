using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Domain.Entities;

namespace AssessmentApplication.Data.Interfaces
{
    public interface ISalesOrderDetailRepository
    {
        Task<SalesOrderDetailEntity> GetSalesOrderDetailAsync(int salesOrderId, CancellationToken cancellationToken);
    }
}