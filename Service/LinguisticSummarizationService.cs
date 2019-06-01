using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LinguisticSummarizationService
    {
        public List<Quantifier> Quantifiers { get; set; }
        public List<Qualifier> Qualifiers { get; set; }
        public int QualifiersMinNumber { get; set; }
        public int QualifiersMaxNumber { get; set; }
        public List<Operation> QualifierOperations { get; set; }
        public List<Summarizer> Summarizers { get; set; }
        public int SummarizersMinNumber { get; set; }
        public int SummarizersMaxNumber { get; set; }
        public List<Operation> SummarizerOperations { get; set; }
        public List<LinguisticSummarization> Summarizations { get; set; }
        public List<Model> Data { get; set; }

        public LinguisticSummarizationService()
        {
            Summarizations = new List<LinguisticSummarization>();
        }

        public List<(string, Result)> Summarize()
        {
            List<(string, Result)> summarizations = new List<(string, Result)>();
            List<List<Summarizer>> summarizersCombinations = GenerateSummarizers();
            List<List<Qualifier>> qualifierCombinations = GenerateQualifiers();
            foreach (var quantifier in Quantifiers)
            {
                foreach (var qualifierCombination in qualifierCombinations)
                {
                    foreach (var summarizersCombination in summarizersCombinations)
                    {
                        if (qualifierCombination.Count > 1)
                        {
                            foreach (var qualifierOperation in QualifierOperations)
                            {
                                if (summarizersCombination.Count > 1)
                                {
                                    foreach (var operation in SummarizerOperations)
                                    {
                                        Summarizations.Add(new LinguisticSummarization() { Quantifier = quantifier, Qualifiers = qualifierCombination, Summarizers = summarizersCombination, Data = Data, OperationQualifier = qualifierOperation, OperationSummarizer = operation });
                                    }
                                }
                                else
                                    Summarizations.Add(new LinguisticSummarization() { Quantifier = quantifier, Qualifiers = qualifierCombination, Summarizers = summarizersCombination, Data = Data, OperationQualifier = qualifierOperation, OperationSummarizer = Operation.None });
                            }

                        }
                        else
                        {
                            if (summarizersCombination.Count > 1)
                            {
                                foreach (var operation in SummarizerOperations)
                                {
                                    Summarizations.Add(new LinguisticSummarization() { Quantifier = quantifier, Qualifiers = qualifierCombination, Summarizers = summarizersCombination, Data = Data, OperationQualifier = Operation.None, OperationSummarizer = operation });
                                }
                            }
                            else
                                Summarizations.Add(new LinguisticSummarization() { Quantifier = quantifier, Qualifiers = qualifierCombination, Summarizers = summarizersCombination, Data = Data, OperationQualifier = Operation.None, OperationSummarizer = Operation.None });
                        }
                    }
                }
            }
            foreach (var summarization in Summarizations)
            {
                string summarizationString = summarization.GenerateSummarization();
                Result summarizationResult = new Result();
                summarizationResult.CalculateResult(summarization);
                summarizations.Add((summarizationString, summarizationResult));
            }
            return summarizations;
        }
        public List<List<Summarizer>> GenerateSummarizers()
        {
            List<List<Summarizer>> summarizersCombinations = new List<List<Summarizer>>();
            Stack<int> combinations = new Stack<int>();
            for (int i = SummarizersMinNumber; i <= Summarizers.Count; i++)
            {
                NextCombination(combinations, i, SummarizersMaxNumber, 1, summarizersCombinations);
            }
            return summarizersCombinations;
        }
        public void NextCombination(Stack<int> combinations, int n, int k, int m, List<List<Summarizer>> summarizersCombinations)
        {
            if (n == 0)
            {
                List<Summarizer> summarizers = new List<Summarizer>();
                foreach (var item in combinations)
                {
                    summarizers.Add(Summarizers[item - 1]);
                }
                summarizersCombinations.Add(summarizers);
            }
            else
                for (int i = m; i <= k; i++)
                {
                    combinations.Push(i);
                    NextCombination(combinations, n - 1, k, i + 1, summarizersCombinations);
                    combinations.Pop();
                }
        }
        public List<List<Qualifier>> GenerateQualifiers()
        {
            List<List<Qualifier>> qualifierCombinations = new List<List<Qualifier>>();
            Stack<int> combinations = new Stack<int>();
            for (int i = QualifiersMinNumber; i <= Qualifiers.Count; i++)
            {
                NextCombinationQualifier(combinations, i, QualifiersMaxNumber, 1, qualifierCombinations);
            }
            return qualifierCombinations;
        }
        public void NextCombinationQualifier(Stack<int> combinations, int n, int k, int m, List<List<Qualifier>> qualifierCombinations)
        {
            if (n == 0)
            {
                List<Qualifier> qualifiers = new List<Qualifier>();
                foreach (var item in combinations)
                {
                    qualifiers.Add(Qualifiers[item - 1]);
                }
                qualifierCombinations.Add(qualifiers);
            }
            else
                for (int i = m; i <= k; i++)
                {
                    combinations.Push(i);
                    NextCombinationQualifier(combinations, n - 1, k, i + 1, qualifierCombinations);
                    combinations.Pop();
                }
        }
    }
}
