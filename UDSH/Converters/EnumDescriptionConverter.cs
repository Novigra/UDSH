using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace UDSH.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            FieldInfo Field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(Field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return (attribute != null) ? attribute.Description : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
