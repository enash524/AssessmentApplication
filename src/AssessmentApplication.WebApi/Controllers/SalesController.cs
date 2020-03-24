using System.Collections.Generic;
using System.Threading.Tasks;
using AssessmentApplication.Application.Sales.SalesOrderDetail.Queries;
using AssessmentApplication.Application.Sales.SalesOrderHeader.Queries;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSalesOrderDetail(int id)
        {
            GetSalesOrderDetailQuery query = new GetSalesOrderDetailQuery
            {
                SalesOrderDetailId = id
            };
            SalesOrderDetailEntity entity = await Mediator.Send(query);
            SalesOrderDetailVm vm = Mapper.Map<SalesOrderDetailVm>(entity);

            return Ok(vm);
        }

        // GET api/Sales/SalesOrderHeader
        [HttpGet("SalesOrderHeader")]
        [ProducesResponseType(typeof(PagedResponse<List<SalesOrderHeaderVm>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSalesOrderHeader()
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