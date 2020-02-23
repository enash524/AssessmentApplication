using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssessmentApplication.Application.Person.Queries;
using AssessmentApplication.Application.SalesOrder.Queries.Detail;
using AssessmentApplication.Application.SalesOrder.Queries.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentApplication.WebApi.Controllers
{
    public class SalesOrderController : BaseController
    {
        // GET api/SalesOrder/Detail/5
        [HttpGet("detail/{salesOrderId:int}")]
        [ProducesResponseType(typeof(SalesOrderDetailVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSalesOrderDetail(int salesOrderId)
        {
            SalesOrderDetailQuery query = new SalesOrderDetailQuery
            {
                SalesOrderDetailId = salesOrderId
            };

            SalesOrderDetailVm result = await Mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/SalesOrder/Search
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<SalesOrderSearchVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSalesOrderSearch([FromQuery] SalesOrderSearchQuery query)
        {
            List<SalesOrderSearchVm> result = await Mediator.Send(query);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/SalesOrder/Test
        [HttpGet("test")]
        [ProducesResponseType(typeof(List<PersonVm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Test()
        {
            GetPersonsQuery query = new GetPersonsQuery();
            List<PersonVm> result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}