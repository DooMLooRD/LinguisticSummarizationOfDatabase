using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MembershipFunction
{
    public class TriangularMembershipFunction : IMembershipFunction
    {
        public List<double> Parameters { get; set; }
        public TriangularMembershipFunction()
        {
            Parameters = new List<double> { 0, 0, 0 };
        }
        public double CalculateMembership(double value)
        {
            if (Parameters.Count != 3)
                throw new ArgumentException("Triangular Membership Function requaires 3 parameters.");

            var start = Parameters[0];
            var pick = Parameters[1];
            var end = Parameters[2];

            if (value > start && value <= pick)
                return (value - start) / (pick - start);
            if (value < end && value > pick)
                return (end - value) / (end - pick);
            return 0;
        }

        public double Support()
        {
            return Math.Abs(Parameters[2] - Parameters[0]) / 2.0;
        }

        public double Cardinality()
        {
            return Math.Abs(Parameters[2] - Parameters[0]);
        }
    }
}
