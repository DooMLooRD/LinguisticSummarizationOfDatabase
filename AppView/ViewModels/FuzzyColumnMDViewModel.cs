using AppView.ViewModels.Base;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using Service.MembershipFunction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace AppView.ViewModels
{
    public class FuzzyColumnMDViewModel : BaseViewModel
    {
        [JsonIgnore]
        public bool IsSelected { get { return SelectedFuzzySet != null; } }
        [JsonIgnore]
        public string Range { get { return $"From {Min} To {Max}"; } }
        public string Name { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public ObservableCollection<FuzzySetCreatorViewModel> FuzzySets { get; set; }
        [JsonIgnore]
        public FuzzySetCreatorViewModel SelectedFuzzySet { get; set; }
        [JsonIgnore]
        public ICommand AddLinguisticValueCommand { get; set; }
        [JsonIgnore]
        public ICommand RemoveLinguisticValueCommand { get; set; }
        [JsonIgnore]
        public ICommand DrawCommand { get; set; }
        [JsonIgnore]
        public SeriesCollection ChartSeries { get; set; }

        public FuzzyColumnMDViewModel()
        {
            ChartSeries = new SeriesCollection();
            FuzzySets = new ObservableCollection<FuzzySetCreatorViewModel>();
            AddLinguisticValueCommand = new RelayCommand(AddLinguisticValue);
            RemoveLinguisticValueCommand = new RelayCommand(RemoveLinguisticValue);
            DrawCommand = new RelayCommand(Draw);
        }
        public void AddLinguisticValue()
        {
            FuzzySets.Add(new FuzzySetCreatorViewModel());
        }
        public void RemoveLinguisticValue()
        {
            FuzzySets.Remove(SelectedFuzzySet);
        }
        public void Draw()
        {
            ChartSeries = new SeriesCollection();

            foreach (var fuzzySet in FuzzySets)
            {
                ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
                if (fuzzySet.MembershipFunction.GetType() == typeof(TriangularMembershipFunction))
                {
                    if (fuzzySet.Start == fuzzySet.Pick)
                    {
                        values.Add(new ObservablePoint(fuzzySet.Start, 1));
                        values.Add(new ObservablePoint(fuzzySet.End, 0));
                    }
                    else if (fuzzySet.Pick == fuzzySet.End)
                    {
                        values.Add(new ObservablePoint(fuzzySet.Start, 0));
                        values.Add(new ObservablePoint(fuzzySet.End, 1));
                    }
                    else
                    {
                        values.Add(new ObservablePoint(fuzzySet.Start, 0));
                        values.Add(new ObservablePoint(fuzzySet.Pick, 1));
                        values.Add(new ObservablePoint(fuzzySet.End, 0));
                    }
                }
                else
                {
                    if (fuzzySet.Start == fuzzySet.MiddleStart)
                    {
                        values.Add(new ObservablePoint(fuzzySet.Start, 1));
                        values.Add(new ObservablePoint(fuzzySet.MiddleEnd, 1));
                        values.Add(new ObservablePoint(fuzzySet.End, 0));
                    }
                    else if (fuzzySet.MiddleEnd == fuzzySet.End)
                    {
                        values.Add(new ObservablePoint(fuzzySet.Start, 0));
                        values.Add(new ObservablePoint(fuzzySet.MiddleStart, 1));
                        values.Add(new ObservablePoint(fuzzySet.End, 1));
                    }
                    else
                    {
                        values.Add(new ObservablePoint(fuzzySet.Start, 0));
                        values.Add(new ObservablePoint(fuzzySet.MiddleStart, 1));
                        values.Add(new ObservablePoint(fuzzySet.MiddleEnd, 1));
                        values.Add(new ObservablePoint(fuzzySet.End, 0));
                    }
                }
                ChartSeries.Add(new LineSeries()
                {
                    LineSmoothness = 0,
                    Fill = Brushes.Transparent,
                    Values = values,
                    Title = fuzzySet.Name
                });
            }
        }
    }
    public enum MembershipEnum
    {
        TRAPEZOIDAL,
        TRIANGULAR
    }

}
