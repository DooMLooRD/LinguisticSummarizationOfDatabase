using Data;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            FuzzyColumnHelper columnHelper = new FuzzyColumnHelper();
            var columns=columnHelper.FuzzyColumns;
        }
    }
}
