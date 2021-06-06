using System;
using Xamarin.Forms;

namespace FitApp.Core
{
    public class SessionViewSelector : DataTemplateSelector
    {
        public DataTemplate ExistingSessionTemplate { get; set; }
        public DataTemplate NewSessionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // latest saved sync point
            int currentVersion = Xamarin.Essentials.Preferences.Get(Constants.DataSyncPointPreference, 0);

            // sync point the session was from
            int trainingSessionVersion = ((TrainingSession)item).LastUpdateVersion;

            // if the sync points are the same - the session is new
            return trainingSessionVersion == currentVersion ? NewSessionTemplate : ExistingSessionTemplate;
        }
    }
}
