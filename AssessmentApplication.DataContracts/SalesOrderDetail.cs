using System;
using Newtonsoft.Json;

namespace AssessmentApplication.DataContracts
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class SalesOrderDetail
    {
        #region Public Properties

        [JsonProperty]
        public decimal LineTotal { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public Int16 OrderQty { get; set; }

        [JsonProperty]
        public string ProductNumber { get; set; }

        [JsonProperty]
        public decimal UnitPrice { get; set; }

        [JsonProperty]
        public decimal UnitPriceDiscount { get; set; }

        #endregion Public Properties
    }
}