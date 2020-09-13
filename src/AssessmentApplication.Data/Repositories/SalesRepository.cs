using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AssessmentApplication.Data.Dto;
using AssessmentApplication.Data.Extensions;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Data.Requests;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.Data.Repositories
{
    public class SalesRepository : BaseRepository, ISalesRepository
    {
        public SalesRepository(IDbConnection dbConnection, ILogger<SalesRepository> logger, IMapper mapper)
            : base(dbConnection, logger, mapper)
        { }

        public async Task<SalesOrderDetailEntity> GetSalesOrderDetailAsync(int salesOrderId, CancellationToken cancellationToken)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@salesOrderId", salesOrderId);

            SalesOrderDetailDto dto = await QuerySingleOrDefaultAsync<SalesOrderDetailDto>("[Sales].[uspGetSalesOrderDetail]", parms, CommandType.StoredProcedure);
            SalesOrderDetailEntity entity = Mapper.Map<SalesOrderDetailEntity>(dto);

            return entity;
        }

        public async Task<PagedResponse<List<SalesOrderHeaderEntity>>> GetSalesOrderHeaderAsync(SalesOrderHeaderRequest request, CancellationToken cancellationToken)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.AddNullableParameter("@orderDateStart", request.OrderDateStart);
            parms.AddNullableParameter("@orderDateEnd", request.OrderDateEnd);
            parms.AddNullableParameter("@dueDateStart", request.DueDateStart);
            parms.AddNullableParameter("@dueDateEnd", request.DueDateEnd);
            parms.AddNullableParameter("@shipDateStart", request.ShipDateStart);
            parms.AddNullableParameter("@shipDateEnd", request.ShipDateEnd);

            if (!string.IsNullOrWhiteSpace(request.CustomerName))
            {
                parms.Add("@customerName", request.CustomerName);
            }

            parms.Add("@limit", request.PageSize);
            parms.Add("@offset", request.CurrentPage);
            parms.Add("@sortBy", request.SortBy);
            parms.Add("@sortDirection", request.SortDirection);
            parms.Add("@recordCount", dbType: DbType.Int32, direction: ParameterDirection.Output, size: int.MaxValue);

            IEnumerable<SalesOrderHeaderDto> dto = await QueryAsync<SalesOrderHeaderDto>("[Sales].[uspGetSalesOrderHeader]", parms, CommandType.StoredProcedure);
            uint recordCount = Convert.ToUInt32(parms.Get<int>("recordCount"));
            List<SalesOrderHeaderEntity> entity = Mapper.Map<List<SalesOrderHeaderEntity>>(dto);
            PagedResponse<List<SalesOrderHeaderEntity>> response = new PagedResponse<List<SalesOrderHeaderEntity>>()
            {
                Data = entity,
                PageSize = Convert.ToUInt32(request.PageSize),
                RecordCount = recordCount,
                SortBy = request.SortBy,
                SortDirection = request.SortDirection
            };

            return response;
        }
    }
}
