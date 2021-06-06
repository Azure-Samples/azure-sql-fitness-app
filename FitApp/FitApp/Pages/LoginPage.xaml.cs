using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FitApp.Core
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = new LoginPageViewModel();
        }
    }
}
