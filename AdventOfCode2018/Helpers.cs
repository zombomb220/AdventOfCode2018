using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018 {
    public static class Helpers
    {
        public static List<List<T>> ReadFileToSpreadsheet<T>(string fileLocation, char splitter = '\t') {

            string line;
            var    table = new List<List<T>>();

            // Read the file and display it line by line.  
            var file = new System.IO.StreamReader(@"../../DataFiles/" + fileLocation);

            while ( (line = file.ReadLine()) != null ) {
                var rows = new List<T>();

                var t = line.Split(splitter);

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