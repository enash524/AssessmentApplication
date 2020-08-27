using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Dto;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Data.Requests;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Data.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<SalesRepository> _logger;
        private readonly IMapper _mapper;

        public SalesRepository(IDbConnection dbConnection, ILogger<SalesRepository> logger, IMapper mapper)
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SalesOrderDetailEntity> GetSalesOrderDetailAsync(int salesOrderId, CancellationToken cancellationToken)
        {
            SalesOrderDetailDto dto;
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@salesOrderId", salesOrderId);

            using (_dbConnection)
            {
                dto = await _dbConnection.QueryFirstOrDefaultAsync<SalesOrderDetailDto>("[Sales].[uspGetSalesOrderDetail]", parms, commandType: CommandType.StoredProcedure);
            }

            SalesOrderDetailEntity entity = _mapper.Map<SalesOrderDetailEntity>(dto);

            return entity;
        }

        public async Task<PagedResponse<List<SalesOrderHeaderEntity>>> GetSalesOrderHeaderAsync(SalesOrderHeaderRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<SalesOrderHeaderDto> dto;
            DynamicParameters parms = new DynamicParameters();

            if (request.OrderDateStart.HasValue)
            {
                parms.Add("@orderDateStart", request.OrderDateStart.Value);
            }

            if (request.OrderDateEnd.HasValue)
            {
                parms.Add("@orderDateEnd", request.OrderDateEnd.Value);
            }

            if (request.DueDateStart.HasValue)
            {
                parms.Add("@dueDateStart", request.DueDateStart.Value);
            }

            if (request.DueDateEnd.HasValue)
            {
                parms.Add("@dueDateEnd", request.DueDateEnd.Value);
            }

            if (request.ShipDateStart.HasValue)
            {
                parms.Add("@shipDateStart", request.ShipDateStart.Value);
            }

            if (request.ShipDateEnd.HasValue)
            {
                parms.Add("@shipDateEnd", request.ShipDateEnd.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.CustomerName))
            {
                parms.Add("@customerName", request.CustomerName);
            }

            parms.Add("@limit", request.PageSize);
            parms.Add("@offset", request.CurrentPage);
            parms.Add("@sortBy", request.SortBy);
            parms.Add("@sortDirection", request.SortDirection);
            parms.Add("@recordCount", dbType: DbType.Int32, direction: ParameterDirection.Output, size: int.MaxValue);

            using (_dbConnection)
            {
                dto = await _dbConnection.QueryAsync<SalesOrderHeaderDto>("[Sales].[uspGetSalesOrderHeader]", parms, commandType: CommandType.StoredProcedure);
            }

            uint recordCount = parms.Get<uint>("recordCount");

            List<SalesOrderHeaderEntity> entity = _mapper.Map<List<SalesOrderHeaderEntity>>(dto);
            PagedResponse<List<SalesOrderHeaderEntity>> response = new PagedResponse<List<SalesOrderHeaderEntity>>()
            {
                PageSize = 10,
                RecordCount = recordCount,
                Data = entity
            };

            return response;
        }
    }
}
