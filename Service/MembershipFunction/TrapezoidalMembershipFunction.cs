using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MembershipFunction
{
    public class TrapezoidalMembershipFunction : IMembershipFunction
    {
        public List<double> Parameters { get; set; }

        public double CalculateMembership(double value)
        {
            if (Parameters.Count != 4)
                throw new ArgumentException("Trapezoidal Membership Function requaires 4 parameters.");

            var start = Parameters[0];
            var startMiddle = Parameters[1];
            var endMiddle = Parameters[2];
            var end = Parameters[3];

            if (value > start && value < startMiddle)
                return (value - start) / (startMiddle - start);
            if (value >= startMiddle && value <= endMiddle)
                return 1;
            if (value < end && value > endMiddle)
                return (end - value) / (end - endMiddle);
            return 0;
        }
    }
}
