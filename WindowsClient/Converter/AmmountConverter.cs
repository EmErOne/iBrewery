using System;
using System.Globalization;
using System.Windows.Data;

namespace IBrewery.Client.Converter
{
    public class AmmountConverter : IValueConverter
    {
        /// <summary>
        /// Double zu String
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {                
               return value.ToString();
            }

            return value;
        }

        /// <summary>
        /// String zu Double
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                String imputString = value.ToString();
                if (Double.TryParse(imputString, out double output))
                {
                    return output;
                }
            }

            return value;
        }
    }
}