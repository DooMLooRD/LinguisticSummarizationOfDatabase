using AppView.ViewModels;
using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AppView.Converters
{
    public class MembershipToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result;
            if ((MembershipEnum)parameter == MembershipEnum.TRAPEZOIDAL && value.GetType() == typeof(TrapezoidalMembershipFunction))
                result = true;
            else if ((MembershipEnum)parameter == MembershipEnum.TRIANGULAR && value.GetType() == typeof(TriangularMembershipFunction))
                result = true;
            else result = false;
            return result ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
