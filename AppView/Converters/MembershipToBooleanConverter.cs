using AppView.ViewModels;
using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AppView.Converters
{
    public class MembershipToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((MembershipEnum)parameter == MembershipEnum.TRAPEZOIDAL && value.GetType() == typeof(TrapezoidalMembershipFunction))
                return true;
            if ((MembershipEnum)parameter == MembershipEnum.TRIANGULAR && value.GetType() == typeof(TriangularMembershipFunction))
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value)
            {
                if ((MembershipEnum)parameter == MembershipEnum.TRAPEZOIDAL)
                    return new TrapezoidalMembershipFunction();
                return new TriangularMembershipFunction();
            }
            return Binding.DoNothing;
        }
    }
}
