using System;
using System.Globalization;
using Xamarin.Forms;

namespace FitApp.Core
{
    public class BorderColorConverter : IValueConverter
    {
        

        public BorderColorConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int currentVersion = Xamarin.Essentials.Preferences.Get(Constants.DataSyncPointPreference, 0);

            int trainingSessionVersion = (int)value;
            
            return trainingSessionVersion == currentVersion ? Color.Green : Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
