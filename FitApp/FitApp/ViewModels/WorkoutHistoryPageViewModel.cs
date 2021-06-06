using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;

namespace FitApp.Core
{
    public class WorkoutHistoryPageViewModel : BaseViewModel
    {
        ILocalDataService localSvc;
        IWebDataService cloudSvc;

        public WorkoutHistoryPageViewModel()
        {
            GetWorkoutHistoryCommand = new Command(async () => await ExecuteGetWorkoutHistoryCommand());
            StartNewWorkoutCommand = new Command(async () => await ExecuteStartWorkoutCommand());
            SignoutCommand = new Command(() => ExecuteSignoutCommand());
            TrainingSessions = new ObservableCollection<TrainingSession>();

            localSvc = DependencyService.Get<ILocalDataService>();
            cloudSvc = DependencyService.Get<IWebDataService>();
        }
       
        bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set => SetProperty(ref isRefreshing, value); }

        ObservableCollection<TrainingSession> trainingSessions;
        public ObservableCollection<TrainingSession> TrainingSessions
        {
            get => trainingSessions;
            set => SetProperty(ref trainingSessions, value);
        }

        public ICommand GetWorkoutHistoryCommand { get; }
        public ICommand StartNewWorkoutCommand { get; }
        public ICommand SignoutCommand { get; }

        async Task ExecuteGetWorkoutHistoryCommand()
        {
            // update from the cloud            
            await cloudSvc.GetTrainingSessions();

            // get the local sessions            
            var localSessions = localSvc.GetLocalSessions();

            TrainingSessions.Clear();

            foreach (var session in localSessions)
            {
                var workoutDate = DateTime.Parse(session.RecordedOn);
                session.RecordedOnDisplay = workoutDate.ToString(@"MMMM dd @ hh:mm tt");
                TrainingSessions.Add(session);
            }

            IsRefreshing = false;
        }

        void ExecuteSignoutCommand()
        {
            Preferences.Remove(Constants.UserIdPreference);
            Preferences.Remove(Constants.DataSyncPointPreference);

            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        async Task ExecuteStartWorkoutCommand()
        {
            var workoutPage = new FitApp.Core.Pages.WorkoutPage();
            await App.Current.MainPage.Navigation.PushModalAsync(workoutPage);
        }
    }
}
