using AppView.ViewModels.Base;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppView.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        public string Summarization { get; set; }
        public Result Result{ get; set; }
    }
}
