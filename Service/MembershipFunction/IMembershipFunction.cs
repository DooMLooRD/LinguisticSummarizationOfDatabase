using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MembershipFunction
{
    public interface IMembershipFunction
    {
        List<double> Parameters { get; set; }
        double CalculateMembership(double value);
        double Support();
        double Cardinality();
    }
}
