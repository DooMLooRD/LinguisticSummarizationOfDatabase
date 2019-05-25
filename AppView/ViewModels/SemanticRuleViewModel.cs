using AppView.ViewModels.Base;
using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppView.ViewModels
{
    public class SemanticRuleViewModel : BaseViewModel
    {
        public string Name { get; set; } = "New label";
        public IMembershipFunction MembershipFunction { get; set; }
        public SemanticRuleViewModel()
        {
            MembershipFunction = new TriangularMembershipFunction();
        }
    }
}
