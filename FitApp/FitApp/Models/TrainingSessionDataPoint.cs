using System;
namespace FitApp.Core
{
    public class TrainingSessionDataPoint
    {
        public int TrainingSessionId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime MeasureTime { get; set; }
        public int HeartRate { get; set; }
    }
}
