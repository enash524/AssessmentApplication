using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace AssessmentApplication.Application.SalesOrder.Queries.Detail
{
    public class SalesOrderDetailHandler : IRequestHandler<SalesOrderDetailQuery, SalesOrderDetailVm>
    {
        private readonly IConfiguration _configuration;

        public SalesOrderDetailHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SalesOrderDetailVm> Handle(SalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SalesOrderDetailVm result = await connection.QueryFirstOrDefaultAsync<SalesOrderDetailVm>("Sales.uspGetSalesOrderDetail", new { salesOrderDetailId = request.SalesOrderDetailId });
                return result;
            }
        }
    }
}
