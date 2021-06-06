using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace FitApp.Core
{
    public class MockLocalDataService : ILocalDataService
    {
        public MockLocalDataService()
        {
        }

        public void DeleteAllLocalSessions()
        {
            // nothing
        }

        public List<TrainingSession> GetLocalSessions()
        {
            var userId = Preferences.Get(Constants.UserIdPreference, "Matt");

            var session1 = new TrainingSession
            {
                Calories = 100,
                Distance = 1000,
                Duration = 6000,
                RecordedOn = "2020-05-15 11:15:00 -07:00",
                Steps = 500,
                UserId = userId,
                WorkoutType = "Run"
            };

            var session2 = new TrainingSession
            {
                Calories = 100,
                Distance = 1000,
                Duration = 6000,
                RecordedOn = "2020-05-15 11:15:00 -07:00",
                Steps = 500,
                UserId = userId,
                WorkoutType = "Run"
            };

            return new List<TrainingSession> { session1, session2 };
        }

        public void SaveSessionFromWeb(List<TrainingSession> sessions, bool fullReload, int currentVersion)
        {
            // nothing
        }
    }
}
