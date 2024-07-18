using Newtonsoft.Json;
using System.Collections.Generic;

namespace EVEMon.XmlGenerator.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class cloneStates
    {
        [JsonProperty]
        public Dictionary<int, int> skills { get; set; }

        [JsonProperty]
        public string internalDescription { get; set; }
    }
}
