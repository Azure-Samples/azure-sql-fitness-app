using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FitApp.Core
{
    public class DataSyncResult
    {
        [JsonProperty("Metadata")]
        public DataSyncMetaData Metadata { get; set; }

        [JsonProperty("Data")]
        public List<TrainingSession> TrainingData { get; set; }
    }
}
