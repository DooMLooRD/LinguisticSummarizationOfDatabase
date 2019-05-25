using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LinguisticVariable
    {
        public string Name { get; set; }
        public List<string> LinguisticValues { get; private set; }
        public List<IMembershipFunction> SemanticRules { get; private set; }
        public LinguisticVariable(string name)
        {
            Name = name;
            LinguisticValues = new List<string>();
            SemanticRules = new List<IMembershipFunction>();
        }
        public void AddLinguisticValue(string label, IMembershipFunction membershipFunction)
        {
            LinguisticValues.Add(label);
            SemanticRules.Add(membershipFunction);
        }
    }
}
