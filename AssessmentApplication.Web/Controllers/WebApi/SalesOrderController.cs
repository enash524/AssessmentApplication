using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AssessmentApplication.Biz;
using AssessmentApplication.DataContracts;
using AssessmentApplication.Models.SalesOrder;

namespace AssessmentApplication.Controllers.WebApi
{
	[RoutePrefix("api/SalesOrder")]
	public class SalesOrderController : ApiController
	{
		#region Private Fields

		private readonly Lazy<ISalesOrderService> _svc;

		#endregion Private Fields

		#region Public Constructors

		public SalesOrderController()
		{
			_svc = new Lazy<ISalesOrderService>(() => new SalesOrderService());
		}

		#endregion Public Constructors

		#region Public Methods

		// GET api/SalesOrder/Detail/5
		[Route("Detail/{salesOrderId:int}")]
		public async Task<IHttpActionResult> GetSalesOrderDetail(int salesOrderId)
		{
			List<SalesOrderDetail> items = await _svc.Value.GetSalesOrderDetailAsync(salesOrderId);

			if (items == null || items.Count == 0)
				return NotFound();

			return Ok(items);
		}

		// GET api/SalesOrder/Search
		[Route("Search")]
		public async Task<IHttpActionResult> GetSalesOrderSearch([FromUri] SalesOrderSearchModel model)
		{
			List<SalesOrderHeader> items = await _svc.Value.GetAllSalesOrderHeaderAsync(model?.CustomerName, model?.DueDateEnd, model?.DueDateStart, model?.OrderDateEnd, model?.OrderDateStart, model?.ShipDateEnd, model?.ShipDateStart);
			return Ok(items);
		}

		#endregion Public Methods
	}
}