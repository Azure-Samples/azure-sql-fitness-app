using System;
using System.Threading.Tasks;

namespace FitApp.Core
{
    public class MockWebDataService : IWebDataService
    {
        public MockWebDataService()
        {
        }

        public async Task GetTrainingSessions()
        {
            await Task.CompletedTask;
        }

        public async Task<bool> SaveTrainingSession(TrainingSessionRequest session)
        {
            return await Task.FromResult(true);
        }
    }
}
