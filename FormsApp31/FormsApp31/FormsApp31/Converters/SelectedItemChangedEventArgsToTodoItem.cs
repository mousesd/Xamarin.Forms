using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using FormsApp31.Models;
using Xamarin.Forms;

namespace FormsApp31.Converters
{
    public class SelectedItemChangedEventArgsToTodoItem : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SelectedItemChangedEventArgs e))
                throw new ArgumentException($"{nameof(value)} type must be {nameof(SelectedItemChangedEventArgs)}");

            return (TodoItem)e.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
