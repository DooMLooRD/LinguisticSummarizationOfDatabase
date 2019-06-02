using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ResultWrtier
    {
        public static async Task WriteToFile(List<(string, Result)> results, string path, bool latexMode)
        {
            results = results.OrderByDescending(c => c.Item2.T).ToList();
            StringBuilder resultText = new StringBuilder();
            if (latexMode)
            {
                resultText.AppendLine(@"\begin{table}[H]");
                resultText.AppendLine(@"\centering \tiny");
                resultText.AppendLine(@"\begin{tabular}{|M{9cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|M{0,4cm}|}");
                resultText.AppendLine(@"\hline");
                resultText.AppendLine(@"Podsumowanie & $T$ & $T_1$ & $T_2$ & $T_3$ & $T_4$ & $T_5$ & $T_6$ & $T_7$ & $T_8$ & $T_9$ & $T_{10}$ & $T_{11}$ \\ \hline");
                foreach (var result in results)
                {
                    resultText.AppendLine($"{result.Item1} & {result.Item2.T} & {result.Item2.T1} & {result.Item2.T2} & {result.Item2.T3} & {result.Item2.T4} & {result.Item2.T5} &" +
                        $" {result.Item2.T6} & {result.Item2.T7} & {result.Item2.T8} & {result.Item2.T9} & {result.Item2.T10} & {result.Item2.T11}" +
                        @"\\ \hline");
                }
                resultText.AppendLine(@"\end{tabular}");
                resultText.AppendLine(@"\caption{Sth}");
                resultText.AppendLine(@"\label{table:1}");
                resultText.AppendLine(@"\end{table}");
            }
            else
            {
                resultText.AppendLine("| Podsumowanie | T | T1 | T2 | T3 | T4 | T5 | T6 | T7 | T8 | T9 | T10 | T11 |");
                foreach (var result in results)
                {
                    resultText.AppendLine($"{result.Item1} | {result.Item2.T} | {result.Item2.T1} | {result.Item2.T2} | {result.Item2.T3} | {result.Item2.T4} | {result.Item2.T5} |" +
                        $" {result.Item2.T6} | {result.Item2.T7} | {result.Item2.T8} | {result.Item2.T9} | {result.Item2.T10} | {result.Item2.T11}");
                }
            }

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                await outputFile.WriteAsync(resultText.ToString());
            }
        }
    }
}
