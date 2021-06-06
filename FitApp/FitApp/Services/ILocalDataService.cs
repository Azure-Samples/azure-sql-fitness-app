using System;
using System.Collections.Generic;

namespace FitApp.Core
{
    public interface ILocalDataService
    {
        public void SaveSessionFromWeb(List<TrainingSession> sessions, bool fullReload, int currentVersion);
        public List<TrainingSession> GetLocalSessions();
        public void DeleteAllLocalSessions();
    }
}
