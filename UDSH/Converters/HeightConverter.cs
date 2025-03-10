// Copyright (C) 2025 Mohammed Kenawy
using System.Globalization;
using System.Windows.Data;

namespace UDSH.Converters
{
    internal class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double parentHeight && double.TryParse(parameter?.ToString(), out double subtractValue))
            {
                return Math.Max(0, parentHeight - subtractValue);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
