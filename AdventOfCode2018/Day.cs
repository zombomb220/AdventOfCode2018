using System;
using System.Diagnostics;

namespace AdventOfCode2018 {
    public abstract class Day
    {
        private Stopwatch s = new Stopwatch();
        private string    cachedResult;

        public void Run()
        {
            s.Start();
            Problem1();
            s.Stop();

            FinalPrint();

            cachedResult = "";
            s.Reset();

            s.Start();
            Problem2();
            s.Stop();

            FinalPrint();

            Console.WriteLine();
        }

        public abstract void Problem1();

        public abstract void Problem2();

        protected void Print(int problemNumber, int answer, int? correctAnswer = null)
        {
            Print(problemNumber, answer.ToString(), correctAnswer.ToString());
        }

        protected void Print(int problemNumber, string answer, string correctAnswer = null)
        {
            if(answer != null && !string.IsNullOrEmpty(correctAnswer))
                cachedResult = this + ": [p"+ problemNumber + " - " + (answer == correctAnswer ? "PASS" : "FAIL") + "] (ms: *)\t = " + answer;
            else if ( answer != null)
                cachedResult = this + ": [p" + problemNumber + "] (ms: *)\t = " + answer;
            else
                cachedResult = this + ": [p" + problemNumber + "] (ms: *)\t - ANSWER NOT FOUND";

        }

        private void FinalPrint()
        {
            if (string.IsNullOrEmpty(cachedResult)) return;

            cachedResult = cachedResult.Replace("*", s.ElapsedMilliseconds.ToString());
            Console.WriteLine(cachedResult);
        }

    }
}