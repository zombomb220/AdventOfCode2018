using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2018 {
    
    class Program {
        static void Main(string[] args) {

            var d1 = new Day1();
            d1.Run();

            var d2 = new Day2();
            d2.Run();

            var d3 = new Day3();
            d3.Run();

            var d4 = new Day4();
            d4.Run();

            var d5 = new Day5();
            d5.Run();


            Console.WriteLine("Complete!");
            Console.Read();
        }
    }

    public abstract class Day
    {
        private Stopwatch s = new Stopwatch();
        private string cachedResult;

        public void Run()
        {
            s.Start();
            Problem1();
            s.Stop();

            FinalPrint();

            s.Reset();

            s.Start();
            Problem2();
            s.Stop();

            FinalPrint();

            Console.WriteLine();
        }

        public abstract void Problem1();

        public abstract void Problem2();

        protected void Print(int problemNumber, string answer, string correctAnswer = null)
        {
            if(answer != null && correctAnswer != null)
                cachedResult = this + ": [p"+ problemNumber + " - " + (answer == correctAnswer ? "PASS" : "FAIL") + "] (ms: *)\t = " + answer;
            else if ( answer != null)
                cachedResult = this + ": [p" + problemNumber + "] (ms: *)\t = " + answer;
            else
                cachedResult = this + ": [p" + problemNumber + "] (ms: *)\t - ANSWER NOT FOUND";

        }

        private void FinalPrint()
        {
            cachedResult = cachedResult.Replace("*", s.ElapsedMilliseconds.ToString());
            Console.WriteLine(cachedResult);
        }

    }

    public static class Helpers
    {
        public static List<List<T>> ReadFileToSpreadsheet<T>(string fileLocation) {

            string line;
            var    table = new List<List<T>>();

            // Read the file and display it line by line.  
            var file = new System.IO.StreamReader(@"../../DataFiles/" + fileLocation);

            while ( (line = file.ReadLine()) != null ) {
                var rows = new List<T>();

                var t = line.Split('\t');

                rows.AddRange(t.Select(t1 => (T)Convert.ChangeType(t1, typeof(T))));

                table.Add(rows);
            }

            file.Close();

            return table;
        }


        //public static List<List<double>> ReadFileToSpreadsheet_double(string fileLocation) {
        //    string line;
        //    var    table = new List<List<double>>();

        //    // Read the file and display it line by line.  
        //    var file = new System.IO.StreamReader(@"../../DataFiles/" + fileLocation);

        //    while ( (line = file.ReadLine()) != null ) {
        //        var rows = new List<double>();

        //        var t = line.Split('\t');

        //        rows.AddRange(t.Select(t1 => Convert.ToDouble(t1)));

        //        table.Add(rows);
        //    }

        //    file.Close();

        //    return table;
        //}

        //public static List<List<string>> ReadFileToSpreadsheet_char(string fileLocation) {
        //    string line;
        //    var    table = new List<List<string>>();

        //    // Read the file and display it line by line.  
        //    var file = new System.IO.StreamReader(@"../../DataFiles/" + fileLocation);

        //    while ( (line = file.ReadLine()) != null ) {
        //        var rows = new List<string>();

        //        var t = line.Split('\t');

        //        rows.AddRange(t.Select(Convert.ToString));

        //        table.Add(rows);
        //    }

        //    file.Close();

        //    return table;
        //}
    }
}