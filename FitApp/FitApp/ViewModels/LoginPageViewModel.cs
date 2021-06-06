using System;
using System.Windows.Input;
using FitApp.Core.Pages;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FitApp.Core
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel()
        {
            SaveUserNameCommand = new Command(() => ExecuteSaveUserName());
        }

        string userName;
        public string UserName { get => userName; set => SetProperty(ref userName, value); }

        public ICommand SaveUserNameCommand { get; }

        void ExecuteSaveUserName()
        {
            // write a user id if there isn't one            
            Preferences.Set(Constants.UserIdPreference, UserName);

            //App.Current.MainPage = new AppShellPage();
            App.Current.MainPage = new NavigationPage(new WorkoutHistoryPage());
        }
    }
}
