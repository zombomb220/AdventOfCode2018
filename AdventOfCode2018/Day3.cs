using System;
using System.Collections.Generic;

namespace AdventOfCode2018 {
    public class Day3 : Day
    {
        private int answer2;
        private int[,] _map;
        public override void Problem1()
        {
            var spreadSheet = Helpers.ReadFileToSpreadsheet<string>("Day3_P1.txt");
            var size = 1000;


            _map = ConstructMap(spreadSheet, size);
            var count = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ( _map[i, j] == -1) count++;
                }
            }

            Print(1, count.ToString(), "104241");

        }

        private int[,] ConstructMap(List<List<string>> spreadSheet, int aSize)
        {
            var numPerf = 0;
            var map = new int[aSize, aSize];

            for (var i = 0; i < spreadSheet.Count; i++)
            {
                var coords = GetCoords(spreadSheet[i][0]);
                var size = GetSize(spreadSheet[i][0]);

                for (var j = coords.Item1; j < coords.Item1+size.Item1 ; j++)
                {
                    for (var k = coords.Item2; k < coords.Item2+size.Item2; k++)
                    {
                        if( map[j,k] == 0)
                            map[j,k] = i+1;
                        else
                            map[j, k] = -1;
                    }
                }
            }

            for ( var i = 0; i < spreadSheet.Count; i++ ) {
                var coords = GetCoords(spreadSheet[i][0]);
                var size   = GetSize(spreadSheet[i][0]);
                
                var perfectFit = true;
                for ( var j = coords.Item1; j < coords.Item1 + size.Item1; j++ ) {
                    for ( var k = coords.Item2; k < coords.Item2 + size.Item2; k++ ) {
                        if ( map[j, k] == -1 )
                            perfectFit = false;
                    }
                }

                if ( perfectFit )
                    numPerf++;

                if ( answer2 == 0 )
                    answer2 = perfectFit ? i + 1 : 0;
            }
            
            return map;
        }

        private Tuple<int, int> GetCoords(string d)
        {

            var s = d.Split('@')[1].Split(':')[0].Split(',');

            return new Tuple<int, int>(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]));
        }

        private Tuple<double, double> GetSize(string d)
        {
            var s = d.Split('@')[1].Split(':')[1].Split('x');

            return new Tuple<double, double>(Convert.ToDouble(s[0]), Convert.ToDouble(s[1]));
        }






        public override void Problem2()
        {
            Print(2, answer2.ToString(),"806");
        }
    }
}