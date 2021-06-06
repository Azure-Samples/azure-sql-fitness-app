using System;
namespace FitApp.Core
{
    public enum WorkoutRoute
    {
        SpaceNeedle,
        Redmond
    }

    public class DisplayWorkoutMessage
    {
        public static readonly string WorkoutChange = "workoutchange";

        public DisplayWorkoutMessage(WorkoutRoute route)
        {
            WorkoutRoute = route;
        }

        public WorkoutRoute WorkoutRoute { get; }
    }
}
