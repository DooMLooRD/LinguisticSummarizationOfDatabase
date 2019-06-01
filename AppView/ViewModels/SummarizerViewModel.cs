using AppView.ViewModels.Base;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppView.ViewModels
{
    public class SummarizerParentViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public List<FuzzySetViewModel> FuzzySets{ get; set; }
    }

}
