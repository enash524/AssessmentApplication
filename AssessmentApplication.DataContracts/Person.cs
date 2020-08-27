using System;
using Newtonsoft.Json;

namespace AssessmentApplication.DataContracts
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Person
    {
        #region Public Properties

        [JsonProperty]
        public int BusinessEntityId { get; set; }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastName { get; set; }

        [JsonProperty]
        public string MiddleName { get; set; }

        [JsonProperty]
        public string Suffix { get; set; }

        [JsonProperty]
        public string Title { get; set; }

        #endregion Public Properties
    }
}
