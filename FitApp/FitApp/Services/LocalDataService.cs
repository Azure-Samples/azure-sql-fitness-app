using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache.SQLite;

namespace FitApp.Core
{
    public class LocalDataService : ILocalDataService
    {
        public LocalDataService()
        {
        }

        public void SaveSessionFromWeb(List<TrainingSession> sessions, bool fullReload, int currentVersion)
        {
            if (fullReload)
            {
                Barrel.Current.EmptyAll();

                foreach (var item in sessions)
                {
                    item.LastUpdateVersion = currentVersion;
                    Barrel.Current.Add(item.Id.ToString(), item, TimeSpan.FromDays(100));
                }
            }
            else
            {
                // expected if there are no new updates
                if (sessions == null)
                    return;

                foreach (var item in sessions)
                {
                    if (item.DataOperation.Equals("I", StringComparison.OrdinalIgnoreCase))
                    {
                        // insert
                        item.LastUpdateVersion = currentVersion;
                        Barrel.Current.Add(item.Id.ToString(), item, TimeSpan.FromDays(100));
                    }
                    else if (item.DataOperation.Equals("U", StringComparison.OrdinalIgnoreCase))
                    {
                        // update - so here delete then insert again
                        Barrel.Current.Empty(item.Id.ToString());

                        item.LastUpdateVersion = currentVersion;
                        Barrel.Current.Add(item.Id.ToString(), item, TimeSpan.FromDays(100));
                    }
                    else if (item.DataOperation.Equals("D", StringComparison.OrdinalIgnoreCase))
                    {
                        // delete
                        Barrel.Current.Empty(item.Id.ToString());
                    }
                }
            }
        }

        public List<TrainingSession>GetLocalSessions()
        {
            List<TrainingSession> localSessions = new List<TrainingSession>();

            var allIds = Barrel.Current.GetKeys(MonkeyCache.CacheState.Active);

            foreach (var id in allIds)
            {
                localSessions.Add(Barrel.Current.Get<TrainingSession>(id));
            }

            return localSessions;
        }

        public void DeleteAllLocalSessions()
        {
            Barrel.Current.EmptyAll();
        }
    }
}
