using System;
using System.Globalization;
using Xamarin.Forms;

namespace FormsApp25.Converters
{
    public class SelectedItemChangedEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SelectedItemChangedEventArgs eventArgs))
                return null;

            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
