using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace 仓库管理系统.Converters
{
    public class EnumToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SkinColorInfo skinColorInfo = new SkinColorInfo();
            Brush s =skinColorInfo.Color;
            if (value != null)
            {
                if (value is Brush)
                {

                }
            }
            /*if (value is SkinColorInfo myEnumValue)
            {
                switch (myEnumValue)
                {
                    case SkinColorInfo.Blue:
                        return Brushes.Blue;
                    case SkinColorInfo.Red:
                        return Brushes.Red;
                    case SkinColorInfo.Green:
                        return Brushes.Green;
                    default:
                        return Brushes.White;
                }
            }*/

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
