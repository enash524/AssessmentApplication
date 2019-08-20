using System;
using Newtonsoft.Json;

namespace AssessmentApplication.DataContracts
{
	[JsonObject(MemberSerialization.OptIn)]
	[Serializable]
	public class ShipMethod
	{
		#region Public Properties

		[JsonProperty]
		public string Name { get; set; }

		[JsonProperty]
		public int ShipMethodId { get; set; }

		#endregion Public Properties
	}
}