using AppView.ViewModels.Base;
using Data;
using Microsoft.Win32;
using Newtonsoft.Json;
using Service;
using Service.MembershipFunction;
using Service.Sets;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AppView.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Properties
        private FuzzyColumnMDViewModel selectedFuzzyColumn;

        public ObservableCollection<FuzzyColumnMDViewModel> FuzzyColumns { get; set; }
        public FuzzyColumnMDViewModel SelectedFuzzyColumn
        {
            get => selectedFuzzyColumn;
            set
            {
                selectedFuzzyColumn = value;
                IsSelected = true;
            }
        }
        public FuzzyColumnMDViewModel Quantifier { get; set; }
        public bool IsSelected { get; set; } = false;

        public ObservableCollection<FuzzySetViewModel> Quantifiers { get; set; }
        public ObservableCollection<SummarizerParentViewModel> Summarizers { get; set; }
        public ObservableCollection<SummarizerParentViewModel> Qualifiers { get; set; }
        public ObservableCollection<ResultViewModel> Results { get; set; }

        public int MinSummarizers { get; set; }
        public int MaxSummarizers { get; set; }
        public int MinQualifiers { get; set; }
        public int MaxQualifiers { get; set; }
        public bool AndSummarizers { get; set; }
        public bool OrSummarizers { get; set; }
        public bool AndQualifiers { get; set; }
        public bool OrQualifiers { get; set; }
        public bool IsSummarizing { get; set; }
        public bool LaTeXMode { get; set; }
        #endregion

        #region Commands
        public ICommand LoadColumnsCommand { get; set; }
        public ICommand LoadFuzzyColumnsCommand { get; set; }
        public ICommand SaveFuzzyColumnsCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand SummarizeCommand { get; set; }
        public ICommand SaveResultCommand { get; set; }
        public ICommand SaveQuantifiersCommand { get; set; }
        public ICommand LoadQuantifiersCommand { get; set; }
        #endregion

        #region Constructor 
        public MainWindowViewModel()
        {
            FuzzyColumns = new ObservableCollection<FuzzyColumnMDViewModel>();
            Results = new ObservableCollection<ResultViewModel>();
            Quantifier = new FuzzyColumnMDViewModel() { Min = 0, Max = 1, Name = "Quantifier" };
            LoadColumnsCommand = new RelayCommand(LoadColumns);
            LoadCommand = new RelayCommand(Load);
            SummarizeCommand = new RelayCommand(Summarize);
            SaveFuzzyColumnsCommand = new RelayCommand(SaveFuzzyColumns);
            LoadFuzzyColumnsCommand = new RelayCommand(LoadFuzzyColumns);
            SaveQuantifiersCommand = new RelayCommand(SaveQuantifiers);
            LoadQuantifiersCommand = new RelayCommand(LoadQuantifiers);
            SaveResultCommand = new RelayCommand(SaveResult);
        }
        #endregion

        #region Serialization
        private void LoadQuantifiers()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Json File(*.json)| *.json",
                RestoreDirectory = true
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return;
            }
            using (StreamReader file = File.OpenText(openFileDialog.FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.All;
                Quantifier = (FuzzyColumnMDViewModel)serializer.Deserialize(file, typeof(FuzzyColumnMDViewModel));
            }
        }

        private void SaveQuantifiers()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Json File(*.json)| *.json",
                RestoreDirectory = true
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return;
            }
            using (StreamWriter file = File.CreateText(saveFileDialog.FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.All;
                serializer.Serialize(file, Quantifier);
            }
        }

        private void LoadFuzzyColumns()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Json File(*.json)| *.json",
                RestoreDirectory = true
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return;
            }
            using (StreamReader file = File.OpenText(openFileDialog.FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.All;
                FuzzyColumns = (ObservableCollection<FuzzyColumnMDViewModel>)serializer.Deserialize(file, typeof(ObservableCollection<FuzzyColumnMDViewModel>));
            }
        }

        private void SaveFuzzyColumns()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Json File(*.json)| *.json",
                RestoreDirectory = true
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return;
            }
            using (StreamWriter file = File.CreateText(saveFileDialog.FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.All;
                serializer.Serialize(file, FuzzyColumns);
            }

        }
        #endregion

        private async void SaveResult()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text File(*.txt)| *.txt",
                RestoreDirectory = true
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return;
            }
            await ResultWrtier.WriteToFile(Results.Select(c => (c.Summarization, c.Result)).ToList(), saveFileDialog.FileName, LaTeXMode);
        }

        private async void Summarize()
        {
            await Task.Run(() =>
            {
                IsSummarizing = true;
                List<Quantifier> quantifiers = Quantifiers.Where(c => c.IsChecked).Select(c => (Quantifier)c.FuzzySet).ToList();
                List<Qualifier> qualifiers = Qualifiers.SelectMany(c => c.FuzzySets.Where(d => d.IsChecked).Select(d => (Qualifier)d.FuzzySet)).ToList();
                List<Summarizer> summarizers = Summarizers.SelectMany(c => c.FuzzySets.Where(d => d.IsChecked).Select(d => (Summarizer)d.FuzzySet)).ToList();
                DataLoader loader = new DataLoader();
                List<Model> data = loader.LoadData();
                List<Operation> qualifiersOperation = new List<Operation>();
                List<Operation> summarizersOperation = new List<Operation>();
                if (AndQualifiers)
                    qualifiersOperation.Add(Operation.And);
                if (OrQualifiers)
                    qualifiersOperation.Add(Operation.Or);
                if (AndSummarizers)
                    summarizersOperation.Add(Operation.And);
                if (OrSummarizers)
                    summarizersOperation.Add(Operation.Or);

                LinguisticSummarizationService service = new LinguisticSummarizationService
                {
                    Quantifiers = quantifiers,
                    Qualifiers = qualifiers,
                    QualifiersMinNumber = MinQualifiers,
                    QualifiersMaxNumber = MaxQualifiers,
                    QualifierOperations = qualifiersOperation,
                    Summarizers = summarizers,
                    SummarizersMinNumber = MinSummarizers,
                    SummarizersMaxNumber = MaxSummarizers,
                    SummarizerOperations = summarizersOperation,
                    Data = data
                };
                var summarization = service.Summarize();

                Results = new ObservableCollection<ResultViewModel>(summarization.Select(c => new ResultViewModel() { Summarization = c.Item1, Result = c.Item2 }));
                IsSummarizing = false;
            });
        }


        private void Load()
        {

            Quantifiers = new ObservableCollection<FuzzySetViewModel>();
            Qualifiers = new ObservableCollection<SummarizerParentViewModel>();
            Summarizers = new ObservableCollection<SummarizerParentViewModel>();
            foreach (var fuzzySet in Quantifier.FuzzySets)
            {
                FuzzySet set = new Quantifier();
                set.Label = fuzzySet.Name;
                if (fuzzySet.MembershipFunction.GetType() == typeof(TriangularMembershipFunction))
                    set.MembershipFunction = new TriangularMembershipFunction() { Parameters = new List<double> { fuzzySet.Start, fuzzySet.Pick, fuzzySet.End } };
                else
                    set.MembershipFunction = new TrapezoidalMembershipFunction() { Parameters = new List<double> { fuzzySet.Start, fuzzySet.MiddleStart, fuzzySet.MiddleEnd, fuzzySet.End } };
                Quantifiers.Add(new FuzzySetViewModel() { IsChecked = false, Name = fuzzySet.Name, FuzzySet = set });
            }
            foreach (var fuzzyColumn in FuzzyColumns)
            {
                List<FuzzySetViewModel> summarizers = new List<FuzzySetViewModel>();
                List<FuzzySetViewModel> qualifiers = new List<FuzzySetViewModel>();
                foreach (var fuzzySet in fuzzyColumn.FuzzySets)
                {
                    FuzzySet summarizer = new Summarizer();
                    FuzzySet qualifier = new Qualifier();
                    summarizer.ColumnName = fuzzyColumn.Name;
                    qualifier.ColumnName = fuzzyColumn.Name;
                    summarizer.Xmax = fuzzyColumn.Max;
                    summarizer.Xmin = fuzzyColumn.Min;
                    qualifier.Xmax = fuzzyColumn.Max;
                    qualifier.Xmin = fuzzyColumn.Min;
                    summarizer.Label = fuzzySet.Name;
                    qualifier.Label = fuzzySet.Name;
                    if (fuzzySet.MembershipFunction.GetType() == typeof(TriangularMembershipFunction))
                    {
                        summarizer.MembershipFunction = new TriangularMembershipFunction() { Parameters = new List<double> { fuzzySet.Start, fuzzySet.Pick, fuzzySet.End } };
                        qualifier.MembershipFunction = new TriangularMembershipFunction() { Parameters = new List<double> { fuzzySet.Start, fuzzySet.Pick, fuzzySet.End } };
                    }
                    else
                    {
                        summarizer.MembershipFunction = new TrapezoidalMembershipFunction() { Parameters = new List<double> { fuzzySet.Start, fuzzySet.MiddleStart, fuzzySet.MiddleEnd, fuzzySet.End } };
                        qualifier.MembershipFunction = new TrapezoidalMembershipFunction() { Parameters = new List<double> { fuzzySet.Start, fuzzySet.MiddleStart, fuzzySet.MiddleEnd, fuzzySet.End } };
                    }
                    summarizers.Add(new FuzzySetViewModel() { IsChecked = false, Name = fuzzySet.Name, FuzzySet = summarizer });
                    qualifiers.Add(new FuzzySetViewModel() { IsChecked = false, Name = fuzzySet.Name, FuzzySet = qualifier });

                }
                Qualifiers.Add(new SummarizerParentViewModel() { Name = fuzzyColumn.Name, FuzzySets = qualifiers });
                Summarizers.Add(new SummarizerParentViewModel() { Name = fuzzyColumn.Name, FuzzySets = summarizers });
            }

        }

        public async void LoadColumns()
        {
            await Task.Run(() =>
            {
                FuzzyColumnHelper columnHelper = new FuzzyColumnHelper();
                FuzzyColumns = new ObservableCollection<FuzzyColumnMDViewModel>(columnHelper.FuzzyColumns.Select(c => new FuzzyColumnMDViewModel()
                {
                    Name = c.Name,
                    Min = c.MinValue,
                    Max = c.MaxValue
                }));

            });

        }
    }
}
