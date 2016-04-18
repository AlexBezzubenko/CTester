using System;
using System.Windows.Data;
using System.Windows.Media;

namespace TestApp.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BooleanBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Brushes.Green;
            }
            else
            {
                return Brushes.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class MyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
               object parameter, System.Globalization.CultureInfo culture)
        {
            
            return values.Clone();
        }
        public object[] ConvertBack(object value, Type[] targetTypes,
               object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}