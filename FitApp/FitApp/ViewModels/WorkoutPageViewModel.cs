using System;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;

namespace FitApp.Core
{
    public class WorkoutPageViewModel : BaseViewModel
    {
        Timer workoutTimer;
        Timer stepTimer;
        DateTime startTime;
        long elapsedMilliseconds = 0;

        IWebDataService webSvc;

        public WorkoutPageViewModel()
        {
            webSvc = DependencyService.Get<IWebDataService>();

            workoutTimer = new Timer(100);
            workoutTimer.Elapsed += OnWorkoutTimedEvent;
            workoutTimer.AutoReset = true;

            stepTimer = new Timer(2000);
            stepTimer.Elapsed += OnStepTimedEvent;
            stepTimer.AutoReset = true;

            EndWorkoutCommand = new Command(async () => await ExecuteEndWorkoutCommand());

            UserId = Preferences.Get(Constants.UserIdPreference, "Matt");

            startTime = DateTime.Now;
            workoutTimer.Enabled = true;
            stepTimer.Enabled = true;
        }

        string timerDisplay;
        public string TimerDisplay { get => timerDisplay; set => SetProperty(ref timerDisplay, value); }

        int numberOfSteps;
        public int NumberOfSteps { get => numberOfSteps; set => SetProperty(ref numberOfSteps, value); }

        int distance;
        public int Distance { get => distance; set => SetProperty(ref distance, value); }

        string currentWorkout;
        public string CurrentWorkout { get => currentWorkout; set => SetProperty(ref currentWorkout, value); }

        string userId;
        public string UserId { get => userId; set => SetProperty(ref userId, value); }

        public ICommand StartWorkoutCommand { get; }
        public ICommand EndWorkoutCommand { get; }

        async Task ExecuteEndWorkoutCommand()
        {
            stepTimer.Enabled = false;
            workoutTimer.Enabled = false;

            stepTimer.Elapsed -= OnStepTimedEvent;
            workoutTimer.Elapsed -= OnWorkoutTimedEvent;


            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await App.Current.MainPage.DisplayAlert("No Internet", "You do not have a connection to the internet", "OK");
                await App.Current.MainPage.Navigation.PopModalAsync(true);
                return;
            }

            var session = new TrainingSessionRequest();

            session.RecordedOn = String.Format(@"{0:yyyy-MM-dd HH:mm:ss z\:00}", DateTime.Now);
            session.UserId = Preferences.Get(Constants.UserIdPreference, "Matt");
            session.Distance = Distance;
            session.Steps = NumberOfSteps;
            double elapsedMillis = (double)elapsedMilliseconds / 1000;
            session.Duration = (int)Math.Round(elapsedMillis);
            session.WorkoutType = "Run";
            
            var success = await webSvc.SaveTrainingSession(session);

            if (!success)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occurred during the save.", "OK");
            }

            await App.Current.MainPage.Navigation.PopModalAsync(true);
        }

        void OnWorkoutTimedEvent(object source, ElapsedEventArgs e)
        {
            elapsedMilliseconds += 100;

            var elapsed = e.SignalTime - startTime;

            TimerDisplay = String.Format(@"{0:mm\:ss\.f}", elapsed);
        }

        void OnStepTimedEvent(object source, ElapsedEventArgs e)
        {
            NumberOfSteps += 2;

            Distance = (int) Math.Round(NumberOfSteps * 1.6);
        }
        
    }
}
