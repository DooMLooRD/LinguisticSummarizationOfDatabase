using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Result
    {
        public double T1 { get; set; }
        public double T2 { get; set; }
        public double T3 { get; set; }
        public double T4 { get; set; }
        public double T5 { get; set; }
        public double T6 { get; set; }
        public double T7 { get; set; }
        public double T8 { get; set; }
        public double T9 { get; set; }
        public double T10 { get; set; }
        public double T11 { get; set; }

        public double T { get; set; }

        private List<double> _summarizerMemberships;
        private List<double> _qualifierMemberships;

        public void CalculateResult(LinguisticSummarization summarization)
        {
            T1 = Math.Round(CalculateT1(summarization), 2);
            T2 = Math.Round(CalculateT2(summarization), 2);
            T3 = Math.Round(CalculateT3(summarization), 2);
            T4 = Math.Round(CalculateT4(summarization), 2);
            T5 = Math.Round(CalculateT5(summarization), 2);
            T6 = Math.Round(CalculateT6(summarization), 2);
            T7 = Math.Round(CalculateT7(summarization), 2);
            T8 = Math.Round(CalculateT8(summarization), 2);

            if (summarization.Qualifiers.Count > 0)
            {
                T9 = Math.Round(CalculateT9(summarization), 2);
                T10 = Math.Round(CalculateT10(summarization), 2);
                T11 = Math.Round(CalculateT11(summarization), 2);
                T = (T1 + T2 + T3 + T4 + T5 + T6 + T7 + T8 + T9 + T10 + T11) / 11.0;
            }
            else
            {
                T = (T1 + T2 + T3 + T4 + T5 + T6 + T7 + T8) / 8.0;
            }
            T = Math.Round(T, 2);

        }
        private double CalculateT1(LinguisticSummarization summarization)
        {
            _summarizerMemberships = new List<double>();
            _qualifierMemberships = new List<double>();
            if (summarization.Qualifiers.Count > 0)
            {
                double sumSW = 0.0;
                double sumW = 0.0;
                foreach (var tuple in summarization.Data)
                {
                    double s = summarization.Summarizers[0].CalculateMembership(GetValue(tuple, summarization.Summarizers[0].ColumnName));
                    double w = summarization.Qualifiers[0].CalculateMembership(GetValue(tuple, summarization.Qualifiers[0].ColumnName));
                    for (int i = 1; i < summarization.Summarizers.Count; i++)
                    {
                        if (summarization.OperationSummarizer == Operation.And)
                            s = Math.Min(s, summarization.Summarizers[i].CalculateMembership(GetValue(tuple, summarization.Summarizers[i].ColumnName)));
                        else
                            s = Math.Max(s, summarization.Summarizers[i].CalculateMembership(GetValue(tuple, summarization.Summarizers[i].ColumnName)));
                    }
                    for (int i = 1; i < summarization.Qualifiers.Count; i++)
                    {
                        if (summarization.OperationQualifier == Operation.And)
                            w = Math.Min(w, summarization.Qualifiers[i].CalculateMembership(GetValue(tuple, summarization.Qualifiers[i].ColumnName)));
                        else
                            w = Math.Max(w, summarization.Qualifiers[i].CalculateMembership(GetValue(tuple, summarization.Qualifiers[i].ColumnName)));
                    }
                    _summarizerMemberships.Add(s);
                    _qualifierMemberships.Add(w);
                    sumSW += Math.Min(s, w);
                    sumW += w;
                }
                double r = sumSW / sumW;
                return summarization.Quantifier.CalculateMembership(r / summarization.Data.Count);
            }
            else
            {
                double sumS = 0.0;
                foreach (var tuple in summarization.Data)
                {
                    double s = summarization.Summarizers[0].CalculateMembership(GetValue(tuple, summarization.Summarizers[0].ColumnName));

                    for (int i = 1; i < summarization.Summarizers.Count; i++)
                    {
                        if (summarization.OperationSummarizer == Operation.And)
                            s = Math.Min(s, summarization.Summarizers[i].CalculateMembership(GetValue(tuple, summarization.Summarizers[i].ColumnName)));
                        else
                            s = Math.Max(s, summarization.Summarizers[i].CalculateMembership(GetValue(tuple, summarization.Summarizers[i].ColumnName)));
                    }
                    _summarizerMemberships.Add(s);
                    _qualifierMemberships.Add(1);
                    sumS += s;
                }
                double r = sumS;
                return summarization.Quantifier.CalculateMembership(r / summarization.Data.Count);
            }
        }
        private double CalculateT2(LinguisticSummarization summarization)
        {
            double mulS = 1.0;
            foreach (var summarizer in summarization.Summarizers)
            {
                mulS *= summarizer.In();
            }
            return 1 - Math.Pow(mulS, 1.0 / summarization.Summarizers.Count);
        }
        private double CalculateT3(LinguisticSummarization summarization)
        {
            double sumT = 0.0;
            double sumH = 0.0;
            for (int i = 0; i < _summarizerMemberships.Count; i++)
            {
                sumT += Math.Min(_summarizerMemberships[i], _qualifierMemberships[i]) > 0 ? 1.0 : 0;
                sumH += _qualifierMemberships[i] > 0 ? 1 : 0;
            }
            if (sumH > 0)
                return sumT / sumH;
            return 0;
        }
        private double CalculateT4(LinguisticSummarization summarization)
        {
            double mulS = 1.0;
            foreach (var summarizer in summarization.Summarizers)
            {
                double sumG = 0.0;
                foreach (var tuple in summarization.Data)
                {
                    sumG += summarizer.CalculateMembership(GetValue(tuple, summarizer.ColumnName)) > 0 ? 1.0 : 0;
                }
                mulS *= (sumG / summarization.Data.Count);
            }
            return Math.Abs(mulS - T3);
        }
        private double CalculateT5(LinguisticSummarization summarization)
        {
            return 2 * Math.Pow(1.0 / 2.0, summarization.Summarizers.Count);
        }
        private double CalculateT6(LinguisticSummarization summarization)
        {
            return 1 - summarization.Quantifier.Supp();
        }
        private double CalculateT7(LinguisticSummarization summarization)
        {
            return 1 - summarization.Quantifier.Cardinality();
        }

        private double CalculateT8(LinguisticSummarization summarization)
        {
            double mulS = 1;
            foreach (var summarizer in summarization.Summarizers)
            {
                mulS *= summarizer.Cardinality() / summarizer.X();
            }
            return 1 - Math.Pow(mulS, 1.0 / summarization.Summarizers.Count);

        }
        private double CalculateT9(LinguisticSummarization summarization)
        {
            double mulW = 1;
            foreach (var qualifier in summarization.Qualifiers)
            {
                mulW *= qualifier.In();
            }
            return 1 - Math.Pow(mulW, 1.0 / summarization.Qualifiers.Count);
        }
        private double CalculateT10(LinguisticSummarization summarization)
        {
            double mulW = 1;
            foreach (var qualifier in summarization.Qualifiers)
            {
                mulW *= qualifier.Cardinality() / qualifier.X();
            }
            return 1 - Math.Pow(mulW, 1.0 / summarization.Qualifiers.Count);
        }
        private double CalculateT11(LinguisticSummarization summarization)
        {
            return 2 * Math.Pow(1.0 / 2.0, summarization.Qualifiers.Count);
        }
        private double GetValue(Model model, string propertyName)
        {
            return (double)Convert.ChangeType(model.GetType().GetProperty(propertyName).GetValue(model), typeof(double));

        }
    }
}
