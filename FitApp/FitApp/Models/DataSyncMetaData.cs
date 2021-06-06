using System;
using Newtonsoft.Json;

namespace FitApp.Core
{
    public class DataSyncMetaData
    {
        [JsonProperty("Sync")]
        public DataSyncInfo Sync { get; set; }
    }
}
