using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace AssessmentApplication.Application.SalesOrder.Queries.Search
{
    public class SalesOrderSearchHandler : IRequestHandler<SalesOrderSearchQuery, List<SalesOrderSearchVm>>
    {
        private readonly IConfiguration _configuration;

        public SalesOrderSearchHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<SalesOrderSearchVm>> Handle(SalesOrderSearchQuery request, CancellationToken cancellationToken)
        {
            using(IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parms = new DynamicParameters();

                parms.Add("@orderDateStart", request.OrderDateStart);
                parms.Add("@orderDateEnd", request.OrderDateEnd);
                parms.Add("@dueDateStart", request.DueDateStart);
                parms.Add("@dueDateEnd", request.DueDateEnd);
                parms.Add("@shipDateStart", request.ShipDateStart);
                parms.Add("@shipDateEnd", request.ShipDateEnd);
                parms.Add("@customerName", request.CustomerName);

                IEnumerable<SalesOrderSearchVm> result = await connection.QueryAsync<SalesOrderSearchVm>("Sales.uspSearchSalesOrderHeader", parms);
                return result?.ToList();
            }
        }
    }
}
