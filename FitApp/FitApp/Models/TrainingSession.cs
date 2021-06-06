using System;
using Newtonsoft.Json;

namespace FitApp.Core
{
    public class TrainingSession
    {
        [JsonProperty("$operation")]
        public string DataOperation { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("RecordedOn")]
        public string RecordedOn { get; set; }

        [JsonProperty("Type")]
        public string WorkoutType { get; set; }

        [JsonProperty("Steps")]
        public long Steps { get; set; }

        [JsonProperty("Distance")]
        public long Distance { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("Duration")]
        public long Duration { get; set; }

        [JsonProperty("Calories")]
        public long Calories { get; set; }

        public int LastUpdateVersion { get; set; }

        public string RecordedOnDisplay { get; set; }
    }
}
