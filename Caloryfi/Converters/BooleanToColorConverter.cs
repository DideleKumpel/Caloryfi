using System.Globalization;

namespace Caloryfi.Converters
{
        public class BooleanToColorConverter : IValueConverter
        {
            public Color TrueColor { get; set; }
            public Color FalseColor { get; set; }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool booleanValue)
                {
                    return booleanValue ? TrueColor : FalseColor;
                }
                return FalseColor;
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
}
