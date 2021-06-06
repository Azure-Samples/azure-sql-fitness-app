using System;
using System.Threading.Tasks;

namespace FitApp.Core
{
    public interface IWebDataService
    {
        public Task GetTrainingSessions();
        public Task<bool> SaveTrainingSession(TrainingSessionRequest session);

    }
}
