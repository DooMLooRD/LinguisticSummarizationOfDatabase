using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LinguisticSummarization
    {
        public Quantifier Quantifier { get; set; }
        public List<Qualifier> Qualifiers { get; set; }
        public List<Summarizer> Summarizers { get; set; }
        public string Subject { get; set; }
        public Operation OperationQualifier { get; set; }
        public Operation OperationSummarizer { get; set; }
        public List<Model> Data { get; set; }
        public bool IsAbsolute { get; set; }

        public string GenerateSummarization()
        {
            StringBuilder result = new StringBuilder(Quantifier.Label);
            result.Append(" days");

            if (Qualifiers.Count > 0)
            {
                result.Append(" being/having ");
                result.Append(Qualifiers[0].Label);
                result.Append(" ");
                result.Append(Qualifiers[0].ColumnName);

                for (int i = 1; i < Qualifiers.Count; i++)
                {
                    if (OperationQualifier == Operation.And)
                        result.Append(" and  ");
                    else
                        result.Append(" or  ");

                    result.Append(Qualifiers[i].Label);
                    result.Append(" ");
                    result.Append(Qualifiers[i].ColumnName);
                }

            }

            result.Append(" are/have ");
            result.Append(Summarizers[0].Label);
            result.Append(" ");
            result.Append(Summarizers[0].ColumnName);

            for (int i = 1; i < Summarizers.Count; i++)
            {
                if (OperationSummarizer == Operation.And)
                    result.Append(" and  ");
                else
                    result.Append(" or  ");
                result.Append(Summarizers[i].Label);
                result.Append(" ");
                result.Append(Summarizers[i].ColumnName);

            }

            return result.ToString();
        }



    }

    public enum Operation
    {
        None,
        And,
        Or,
    }
}
