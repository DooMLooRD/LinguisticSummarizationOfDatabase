using AppView.ViewModels.Base;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppView.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private FuzzyColumnMDViewModel selectedFuzzyColumn;

        public List<FuzzyColumnMDViewModel> FuzzyColumns { get; set; }
        public FuzzyColumnMDViewModel SelectedFuzzyColumn
        {
            get => selectedFuzzyColumn;
            set
            {
                selectedFuzzyColumn = value;
                IsSelected = true;
            }
        }
        public bool IsSelected { get; set; } = false;
        public ICommand LoadColumnsCommand { get; set; }
        public MainWindowViewModel()
        {
            FuzzyColumns = new List<FuzzyColumnMDViewModel>();
            LoadColumnsCommand = new RelayCommand(LoadColumns);
        }
        public async void LoadColumns()
        {
            await Task.Run(() =>
            {
                FuzzyColumnHelper columnHelper = new FuzzyColumnHelper();
                FuzzyColumns = columnHelper.FuzzyColumns.Select(c => new FuzzyColumnMDViewModel()
                {
                    Name = c.Name,
                    Min = c.MinValue,
                    Max = c.MaxValue
                }).ToList();

            });

        }
    }
}
