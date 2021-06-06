using System;
using FitApp.Core.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using FitApp.Core;
using MonkeyCache.SQLite;

[assembly: ExportFont("segoeui.ttf")]
[assembly: ExportFont("segoeuib.ttf", Alias ="segoebold")]
[assembly: ExportFont("SegMDL2.ttf", Alias = "mdl")]
[assembly: ExportFont("fa-brands.otf", Alias = "fontawesomebrands")]
[assembly: ExportFont("fa-regular.otf", Alias = "fontawesome")]
[assembly: ExportFont("fa-solid.otf", Alias ="fontawesomesolid")]
namespace FitApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //DependencyService.Register<IWebDataService, MockWebDataService>();
            //DependencyService.Register<ILocalDataService, MockLocalDataService>();

            DependencyService.Register<IWebDataService, WebDataService>();
            DependencyService.Register<ILocalDataService, LocalDataService>();
          
            if (string.IsNullOrEmpty(Preferences.Get(Constants.UserIdPreference, string.Empty)))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new WorkoutHistoryPage());                
            }

            Barrel.ApplicationId = "fitapp";
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
