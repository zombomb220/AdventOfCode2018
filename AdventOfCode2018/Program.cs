﻿using System;
using System.Collections.Generic;
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


            Console.WriteLine("Complete!");
            Console.Read();
        }
    }

    public abstract class Day
    {
        public void Run()
        {
            Problem1();
            Problem2();
        }

        public abstract void Problem1();

        public abstract void Problem2();

        protected void Print(int problemNumber, string answer, string correctAnswer = null)
        {
            if(answer != null && correctAnswer != null)
                Console.WriteLine(this + ": [p"+ problemNumber + " - " + (answer == correctAnswer ? "PASS" : "FAIL") +  "] = " + answer);
            else if ( answer != null)
                Console.WriteLine(this + ": [p" + problemNumber + "] = " + answer);
            else
                Console.WriteLine(this + ": [p" + problemNumber + "] - ANSWER NOT FOUND");

            if(problemNumber == 2)
                Console.WriteLine();
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