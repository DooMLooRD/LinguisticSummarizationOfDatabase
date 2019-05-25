using AppView.ViewModels.Base;
using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppView.ViewModels
{
    public class FuzzyColumnMDViewModel : BaseViewModel
    {
        public bool IsSelected { get { return SelectedSemanticRule != null; } }
        public string Range { get { return $"From {Min} To {Max}"; } }
        public string Name { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public bool IsChecked { get; set; }
        public ObservableCollection<SemanticRuleViewModel> SemanticRules { get; set; }
        public SemanticRuleViewModel SelectedSemanticRule { get; set; }
        public ICommand AddLinguisticValueCommand { get; set; }
        public ICommand RemoveLinguisticValueCommand { get; set; }

        public FuzzyColumnMDViewModel()
        {
            SemanticRules = new ObservableCollection<SemanticRuleViewModel>();
            AddLinguisticValueCommand = new RelayCommand(AddLinguisticValue);
            RemoveLinguisticValueCommand = new RelayCommand(RemoveLinguisticValue);
        }
        public void AddLinguisticValue()
        {
            SemanticRules.Add(new SemanticRuleViewModel());
        }
        public void RemoveLinguisticValue()
        {
            SemanticRules.Remove(SelectedSemanticRule);
        }
    }
    public enum MembershipEnum
    {
        TRAPEZOIDAL,
        TRIANGULAR
    }

}
