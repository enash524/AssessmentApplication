using System.Collections.Generic;
using System.Threading.Tasks;
using AssessmentApplication.Application.Models;
using AssessmentApplication.Application.Queries.Sales.SalesOrderDetail;
using AssessmentApplication.Application.Queries.Sales.SalesOrderHeader;
using AssessmentApplication.Domain.Common;
using AssessmentApplication.Domain.Entities;
using AssessmentApplication.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentApplication.WebApi.Controllers
{
    public class SalesController : BaseController
    {
        // GET api/Sales/Detail/5
        [HttpGet("Detail/{id:int}")]
        [ProducesResponseType(typeof(SalesOrderDetailVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SalesOrderDetailVm>> GetSalesOrderDetail(int id)
        {
            GetSalesOrderDetailQuery query = new GetSalesOrderDetailQuery
            {
                SalesOrderDetailId = id
            };
            QueryResult<SalesOrderDetailEntity> entity = await Mediator.Send(query);

            if (entity.QueryResultType == QueryResultType.Invalid)
            {
                return BadRequest();
            }

            if (entity.Result == null || entity.QueryResultType == QueryResultType.NotFound)
            {
                return NotFound();
            }

            SalesOrderDetailVm vm = Mapper.Map<SalesOrderDetailVm>(entity.Result);

            return Ok(vm);
        }

        // GET api/Sales/SalesOrderHeader
        [HttpGet("SalesOrderHeader")]
        [ProducesResponseType(typeof(PagedResponse<List<SalesOrderHeaderVm>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResponse<List<SalesOrderHeaderVm>>>> GetSalesOrderHeader()
        {
            GetSalesOrderHeaderQuery query = new GetSalesOrderHeaderQuery();
            PagedResponse<List<SalesOrderHeaderEntity>> entity = await Mediator.Send(query);

            if (entity.Data == null || entity.Data.Count == 0)
            {
                return NotFound();
            }

            PagedResponse<List<SalesOrderHeaderVm>> vm = Mapper.Map<PagedResponse<List<SalesOrderHeaderVm>>>(entity);

            return Ok(vm);
        }
    }
}