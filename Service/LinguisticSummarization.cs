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
        public Operation Operation { get; set; }
        public List<Model> Data { get; set; }

        public string GenerateSummarization()
        {
            string result = Quantifier.Label + " days";

            if (Qualifiers.Count > 0)
            {
                result += " being/having " + Qualifiers[0].Label;

                for (int i = 1; i < Qualifiers.Count; i++)
                {
                    if (Operation == Operation.And)
                    {
                        result += " and  " + Qualifiers[i].Label;
                    }
                    else
                    {
                        result += " or " + Qualifiers[i].Label;
                    }
                }

            }

            result += " are/have " + Summarizers[0].Label;

            for (int i = 1; i < Summarizers.Count; i++)
            {
                if (Operation == Operation.And)
                {
                    result += " and  " + Summarizers[i].Label;
                }
                else
                {
                    result += " or " + Summarizers[i].Label;
                }
            }


            return result;
        }



    }

    public enum Operation
    {
        And,
        Or,
    }
}
