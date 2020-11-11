using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace IBrewery.Client.Converter
{
    class ProgressForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = (double)value;
            Brush foreground = Brushes.LightBlue;



            if (progress >= 90d)
            {
                foreground = Brushes.Red;
            }
            else if (progress >= 75d)
            {
                foreground = Brushes.OrangeRed;
            }
            else if (progress >= 60d)
            {
                foreground = Brushes.Orange;
            }
            else if (progress >= 45d)
            {
                foreground = Brushes.DarkGreen;
            }
            else if (progress >= 30d)
            {
                foreground = Brushes.Green;
            }

            return foreground;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
