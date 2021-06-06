using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FitApp.Core.Pages
{
    public partial class WorkoutHistoryPage : ContentPage
    {
        WorkoutHistoryPageViewModel vm;

        public WorkoutHistoryPage()
        {
            InitializeComponent();

            vm = new WorkoutHistoryPageViewModel();

            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.GetWorkoutHistoryCommand.Execute(null);
        }
    }
}
