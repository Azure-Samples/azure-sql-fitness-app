    using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Net.Http.Headers;
using System.Text;

namespace FitApp.Core
{
    public class WebDataService : IWebDataService
    {
        string syncRequestUrl = "https://fitappbuild.azurewebsites.net/trainingsession/sync";
        string saveRequestUrl = "https://fitappbuild.azurewebsites.net/trainingsession/record";

        static HttpClient client;

        public WebDataService()
        {
            client = new HttpClient();
        }

        public async Task GetTrainingSessions()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await Shell.Current.DisplayAlert("No Internet", "You do not have a connection to the internet","OK");
                return;
            }

            try
            {
                // first pull out the latest saved sync point with 0 being the start
                var syncPoint = Preferences.Get(Constants.DataSyncPointPreference, 0);

                // get the user name - "Matt" will be seed data if nothing else
                var userName = Preferences.Get(Constants.UserIdPreference, "Matt");

                // create the sync request
                var syncRequest = new SyncRequest { FromVersion = syncPoint, UserId = userName };

                // perform the request
                var request = new HttpRequestMessage(HttpMethod.Post, syncRequestUrl);

                request.Content = new StringContent(JsonConvert.SerializeObject(syncRequest), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                // pull out the info
                var syncResultJson = await response.Content.ReadAsStringAsync();
                var syncResult = JsonConvert.DeserializeObject<DataSyncResult>(syncResultJson);

                // save the sync point
                Preferences.Set(Constants.DataSyncPointPreference, (int)syncResult.Metadata.Sync.Version);

                // save training sessions locally
                var localData = new LocalDataService();
                var isFull = syncResult.Metadata.Sync.Type.Equals("full", StringComparison.OrdinalIgnoreCase);
                localData.SaveSessionFromWeb(syncResult.TrainingData, isFull, (int)syncResult.Metadata.Sync.Version);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }        

        public async Task<bool> SaveTrainingSession(TrainingSessionRequest session)
        {            
            try
            {                
                // get the user name - "Matt" will be seed data if nothing else
                var userName = Preferences.Get(Constants.UserIdPreference, "Matt");
                
                // perform the request
                var request = new HttpRequestMessage(HttpMethod.Post, saveRequestUrl);
                                
                request.Content = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                return true;
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

                return false;
            }
        }
    }
}
