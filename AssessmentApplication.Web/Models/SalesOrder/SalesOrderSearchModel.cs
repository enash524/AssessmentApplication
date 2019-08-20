using System;
using Newtonsoft.Json;

namespace AssessmentApplication.Models.SalesOrder
{
	[JsonObject(MemberSerialization.OptIn)]
	[Serializable]
	public class SalesOrderSearchModel
	{
		#region Public Properties

		[JsonProperty]
		public string CustomerName { get; set; }

		[JsonProperty]
		public DateTime? DueDateEnd { get; set; }

		[JsonProperty]
		public DateTime? DueDateStart { get; set; }

		[JsonProperty]
		public DateTime? OrderDateEnd { get; set; }

		[JsonProperty]
		public DateTime? OrderDateStart { get; set; }

		[JsonProperty]
		public DateTime? ShipDateEnd { get; set; }

		[JsonProperty]
		public DateTime? ShipDateStart { get; set; }

		#endregion Public Properties
	}
}