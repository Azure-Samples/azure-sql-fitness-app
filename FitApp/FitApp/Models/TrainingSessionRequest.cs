using System;
namespace FitApp.Core
{
    public class TrainingSessionRequest
    {
        public string RecordedOn { get; set; }

        public string WorkoutType { get; set; }

        public long Steps { get; set; }

        public long Distance { get; set; }

        public long Duration { get; set; }

        public long Calories { get; set; }

        public string UserId { get; set; }
    }
}
