using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Dto;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using Dapper;

namespace AssessmentApplication.Data.Repositories
{
    public class SalesOrderDetailRepository : ISalesOrderDetailRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public SalesOrderDetailRepository(IDbConnection dbConnection, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<SalesOrderDetailEntity> GetSalesOrderDetailAsync(int salesOrderId, CancellationToken cancellationToken)
        {
            using (_dbConnection)
            {
                DynamicParameters parms = new DynamicParameters();

                parms.Add("@salesOrderId", salesOrderId);

                SalesOrderDetailDto dto = await _dbConnection.QueryFirstAsync<SalesOrderDetailDto>("[Sales].[uspGetSalesOrderDetail]", parms, commandType: CommandType.StoredProcedure);
                SalesOrderDetailEntity entity = _mapper.Map<SalesOrderDetailEntity>(dto);

                return entity;
            }
        }
    }
}
