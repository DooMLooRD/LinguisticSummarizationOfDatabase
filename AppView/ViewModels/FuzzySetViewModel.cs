using Service.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppView.ViewModels
{
    public class FuzzySetViewModel
    {
        public bool IsChecked { get; set; }
        public string Name { get; set; }
        public FuzzySet FuzzySet { get; set; }
    }
}
