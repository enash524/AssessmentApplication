using System;
using Newtonsoft.Json;

namespace AssessmentApplication.DataContracts
{
	[JsonObject(MemberSerialization.OptIn)]
	[Serializable]
	public class SalesOrderHeader
	{
		#region Public Properties

		[JsonProperty]
		public string AccountNumber { get; set; }

		[JsonProperty]
		public decimal Freight { get; set; }

		[JsonProperty]
		public Person Person { get; set; }

		[JsonProperty]
		public int SalesOrderId { get; set; }

		[JsonProperty]
		public ShipMethod ShipMethod { get; set; }

		[JsonProperty]
		public Address ShipToAddress { get; set; }

		[JsonProperty]
		public decimal SubTotal { get; set; }

		[JsonProperty]
		public decimal TaxAmt { get; set; }

		[JsonProperty]
		public decimal TotalDue { get; set; }

		#endregion Public Properties
	}
}