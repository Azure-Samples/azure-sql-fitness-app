using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FitApp.Core.Pages
{
    public partial class WorkoutPage : ContentPage
    {
        public WorkoutPage()
        {
            InitializeComponent();

            BindingContext = new WorkoutPageViewModel();
        }        
    }
}
