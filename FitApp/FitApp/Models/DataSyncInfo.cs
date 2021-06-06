using System;
using Newtonsoft.Json;

namespace FitApp.Core
{
    public class DataSyncInfo
    {
        [JsonProperty("Version")]
        public long Version { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("ReasonCode")]
        public long ReasonCode { get; set; }
    }
}
