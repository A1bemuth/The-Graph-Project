using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace UI.Converters
{
    public class CycleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cycles = value as IEnumerable<IEnumerable<int>>;
            if (cycles == null)
                return value;
            return cycles.Select(c => string.Join(",", c));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}