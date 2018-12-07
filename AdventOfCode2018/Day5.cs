using System;

namespace AdventOfCode2018 {
    public class Day5 : Day
    {
        public override void Problem1() {

            var input = Helpers.ReadFileToSpreadsheet<string>("Day5_P1.txt");
            
            var str = input[0][0];
            React(ref str);
        
            Print(1, str.Length.ToString(), "11364");
        }

        private bool React(ref string map) {

            var requiresAdditionalIteration = false;

            for ( var i = 0; i < map.Length - 1; i++ ) {

                if ( Math.Abs(Convert.ToInt32(map[i]) - Convert.ToInt32(map[i + 1])) == 32 ) {
                    RemovePair(ref map, i);
                    requiresAdditionalIteration = true;
                }
            }

            if ( requiresAdditionalIteration )
                return !React(ref map);

            return false;
        }

        private void RemovePair(ref string map, int leftIndex)
        {
            map = map.Remove(leftIndex, 2);
        }


        public override void Problem2()
        {
            var input = Helpers.ReadFileToSpreadsheet<string>("Day5_P1.txt");
            var str = input[0][0];

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var min = int.MaxValue;

            for (int i = 0; i < chars.Length; i++)
            {
                var newTmpStr = str.Replace(chars[i].ToString().ToLower(), "");
                newTmpStr = newTmpStr.Replace(chars[i].ToString().ToUpper(), "");

                React(ref newTmpStr);
                var amt = newTmpStr.Length;

                if (amt < min )
                    min = amt;
                
            }

            Print(2, min.ToString(), "4212");

        }

    }
}