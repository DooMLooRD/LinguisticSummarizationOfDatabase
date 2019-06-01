using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utilities
{
    public class FuzzyColumnHelper
    {
        public List<FuzzyColumn> FuzzyColumns { get; set; }
        public FuzzyColumnHelper()
        {
            FuzzyColumns = new List<FuzzyColumn>();
            InitColumns();
        }
        private void InitColumns()
        {
            DataLoader loader = new DataLoader();
            var data = loader.LoadData();
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.MinTemp), MinValue = data.Min(c => c.MinTemp).Value, MaxValue = data.Max(c => c.MinTemp).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.MaxTemp), MinValue = data.Min(c => c.MaxTemp).Value, MaxValue = data.Max(c => c.MaxTemp).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Rainfall), MinValue = data.Min(c => c.Rainfall).Value, MaxValue = data.Max(c => c.Rainfall).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Evaporation), MinValue = data.Min(c => c.Evaporation).Value, MaxValue = data.Max(c => c.Evaporation).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Sunshine), MinValue = data.Min(c => c.Sunshine).Value, MaxValue = data.Max(c => c.Sunshine).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.WindGustSpeed), MinValue = data.Min(c => c.WindGustSpeed).Value, MaxValue = data.Max(c => c.WindGustSpeed).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.WindSpeed9am), MinValue = data.Min(c => c.WindSpeed9am).Value, MaxValue = data.Max(c => c.WindSpeed9am).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.WindSpeed3pm), MinValue = data.Min(c => c.WindSpeed3pm).Value, MaxValue = data.Max(c => c.WindSpeed3pm).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Humidity9am), MinValue = data.Min(c => c.Humidity9am).Value, MaxValue = data.Max(c => c.Humidity9am).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Humidity3pm), MinValue = data.Min(c => c.Humidity3pm).Value, MaxValue = data.Max(c => c.Humidity3pm).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Pressure9am), MinValue = data.Min(c => c.Pressure9am).Value, MaxValue = data.Max(c => c.Pressure9am).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Pressure3pm), MinValue = data.Min(c => c.Pressure3pm).Value, MaxValue = data.Max(c => c.Pressure3pm).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Cloud9am), MinValue = data.Min(c => c.Cloud9am).Value, MaxValue = data.Max(c => c.Cloud9am).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Cloud3pm), MinValue = data.Min(c => c.Cloud3pm).Value, MaxValue = data.Max(c => c.Cloud3pm).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Temp9am), MinValue = data.Min(c => c.Temp9am).Value, MaxValue = data.Max(c => c.Temp9am).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Temp3pm), MinValue = data.Min(c => c.Temp3pm).Value, MaxValue = data.Max(c => c.Temp3pm).Value });
            FuzzyColumns.Add(new FuzzyColumn { Name = nameof(Model.Risk_mm), MinValue = data.Min(c => c.Risk_mm).Value, MaxValue = data.Max(c => c.Risk_mm).Value });
        }
    }

    public class FuzzyColumn
    {
        public string Name { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
