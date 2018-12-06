
namespace AdventOfCode2018 {
    public class Day2 : Day
    {
        public override void Problem1() 
        {
            var spreadsheet = Helpers.ReadFileToSpreadsheet<string>("Day2_P1.txt");

            var num2 = 0;
            var num3 = 0;

            //for each line
            for (int i = 0; i < spreadsheet.Count; i++)
            {
                var currentLine = spreadsheet[i][0];

                var _num2 = 0;
                var _num3 = 0;

                //for each char
                for (int j = 0; j < currentLine.Length; j++)
                {
                    var currentChar = currentLine[j];

                    var len = 0;

                    for (int k = 0; k < currentLine.Length; k++)
                    {
                        if (currentLine[k] == currentChar) len++;
                    }

                    _num2 += len == 2 ? 1 : 0;
                    _num3 += len == 3 ? 1 : 0;
                }


                num2 += _num2 > 0 ? 1 : 0;
                num3 += _num3 > 0 ? 1 : 0;
            }

            Print(1, (num2 * num3).ToString(), (8610).ToString());
        }

        public override void Problem2()
        {
            var spreadsheet = Helpers.ReadFileToSpreadsheet<string>("Day2_P1.txt");

            for (int i = 0; i < spreadsheet.Count; i++)
            {
                var currentRow = spreadsheet[i][0];

                for (int j = 0; j < spreadsheet.Count; j++)
                {
                    var compareRow = spreadsheet[j][0];

                    var diffScore = Score(currentRow, compareRow);

                    if (diffScore != 1) continue;

                    var newString = "";

                    for (int k = 0; k < currentRow.Length; k++)
                    {
                        if( currentRow[k] == compareRow[k] )
                            newString += currentRow[k];
                    }

                    Print(2, newString, "iosnxmfkpabcjpdywvrtahluy");
                    return;
                }
            }

            Print(2, "FAIL");
        }

        private int Score(string currentRow, string otherRow)
        {
            var dif = 0;

            for (int i = 0; i < currentRow.Length; i++)
            {
                dif += currentRow[i] != otherRow[i] ? 1 : 0;
            }

            return dif;
        }
    }
}