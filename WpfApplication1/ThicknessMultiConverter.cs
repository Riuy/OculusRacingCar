using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OculusRacingCar
{
    public class ThicknessMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var factor = System.Convert.ToDouble(parameter);
            var horizontal = System.Convert.ToDouble(values[0]) * factor;
            var vertical = System.Convert.ToDouble(values[1]) * factor;
            return new Thickness(horizontal, vertical, -horizontal, -vertical);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
