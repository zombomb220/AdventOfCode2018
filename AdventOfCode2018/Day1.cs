using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018 {

    public class Day1 : Day
    {
        public override void Problem1() 
        {
            var spreadsheet = Helpers.ReadFileToSpreadsheet<double>("Day1_P1.txt");

            var n = spreadsheet.Sum(t => t[0]);

            Print(1, n.ToString(), (538).ToString());
        }

        public override void Problem2()
        {
            var spreadsheet = Helpers.ReadFileToSpreadsheet<double>("Day1_P1.txt");
            var n = 0d;
            var h = new HashSet<double> { n };

            double? ans = null;

            for (var j = 0; j < 999; j++)
            {
                for (var i = 0; i < spreadsheet.Count; i++)
                {
                    n += spreadsheet[i][0];
                    
                    if (h.Contains(n))
                    {
                        ans = n;
                        break;
                    }

                    h.Add(n);
                }

                if (ans != null) break;
            }

            Print(2, ans.ToString(), (77271).ToString());
        }
    }
}