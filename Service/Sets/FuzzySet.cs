using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sets
{
    public abstract class FuzzySet
    {
        public string ColumnName { get; set; }
        public double Xmin { get; set; }
        public double Xmax { get; set; }

        public string Label { get; set; }
        public IMembershipFunction MembershipFunction { get; set; }

        public double CalculateMembership(double value)
        {
            return MembershipFunction.CalculateMembership(value);
        }

        public double X()
        {
            return Math.Abs(Xmax - Xmin);
        }
        public double Supp()
        {
            return MembershipFunction.Support();
        }
        public double Cardinality()
        {
            return MembershipFunction.Cardinality();
        }
        public double In()
        {
            return Supp() / X();
        }

    }
}
