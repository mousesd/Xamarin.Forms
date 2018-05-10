using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FormsApp30.Converters
{
    public class ItemTappedEventArgsToMenuItem : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ItemTappedEventArgs e))
                throw new ArgumentException($"{nameof(value)} type must be {nameof(ItemTappedEventArgs)}");

            return (Models.MenuItem)e.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
