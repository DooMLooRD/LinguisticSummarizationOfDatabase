using AppView.ViewModels.Base;
using Newtonsoft.Json;
using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppView.ViewModels
{
    [JsonObject(IsReference = true)]
    public class FuzzySetCreatorViewModel : BaseViewModel
    {
        public string Name { get; set; } = "New label";
        public IMembershipFunction MembershipFunction { get; set; }
        public double Start{ get; set; }
        public double MiddleStart { get; set; }
        public double Pick{ get; set; }
        public double MiddleEnd { get; set; }
        public double End{ get; set; }
        public FuzzySetCreatorViewModel()
        {
            MembershipFunction = new TriangularMembershipFunction();
        }
    }
}
