using System;
using Newtonsoft.Json;

namespace AssessmentApplication.DataContracts
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Address
    {
        #region Public Properties

        [JsonProperty]
        public int AddressId { get; set; }

        [JsonProperty]
        public string AddressLine1 { get; set; }

        [JsonProperty]
        public string AddressLine2 { get; set; }

        [JsonProperty]
        public string City { get; set; }

        [JsonProperty]
        public string PostalCode { get; set; }

        [JsonProperty]
        public string StateProvinceCode { get; set; }

        #endregion Public Properties
    }
}
