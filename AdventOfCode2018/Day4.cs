using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018 {
    public class Day4 : Day {

        public override void Problem1() {

            var spreadSheet = Helpers.ReadFileToSpreadsheet<string>("Day4_P1.txt");
            Dictionary<int, SleepSummary> summaryList = ProcessRawData(spreadSheet);

            var guards = summaryList.Values.ToList();

            var guardMostAsleep = guards.Find(a => a.TotalTimeAsleep == guards.Max(b => b.TotalTimeAsleep));

            var mostMinAsleep = guardMostAsleep.MinMostAsleep().Item1;

            Print(1, (mostMinAsleep * guardMostAsleep.ID).ToString(), "95199");
        }


        public override void Problem2()
        {
            var spreadSheet = Helpers.ReadFileToSpreadsheet<string>("Day4_P1.txt");
            var summaryList = ProcessRawData(spreadSheet);


            var guards = summaryList.Values.ToList();


            var g = guards.FindIndex(b => b.MinMostAsleep().Item2 == guards.Max(a => a.MinMostAsleep().Item2));
            var t = guards[g].MinMostAsleep();

            Print(2, (t.Item1 * guards[g].ID).ToString(), "7887");
        }


        private Dictionary<int, SleepSummary> ProcessRawData(List<List<string>> spreadSheet) {
            var eventList = new List<GuardEvent>();

            for ( int i = 0; i < spreadSheet.Count; i++ ) {

                var gEvent = new GuardEvent {
                    DT = GetDateInfo(spreadSheet[i][0]),
                    Action = GetGuardAction(spreadSheet[i][0]),
                    RawString = spreadSheet[i][0]
                };

                eventList.Add(gEvent);
            }

            eventList.Sort((e, g) => e.DT.CompareTo(g.DT));


            //Assign IDs
            var id = -1;
            for ( int i = 0; i < eventList.Count; i++ ) {
                if ( eventList[i].Action == GuardAction.StartShift )
                    id = GetGuardID(eventList[i].RawString);

                eventList[i].GuardID = id;
            }


            //Order sequences of sleep events
            var sleepAnalysis = new List<SleepEvent>();

            SleepEvent se = null;
            for ( int i = 0; i < eventList.Count; i++ ) {
                if ( eventList[i].Action == GuardAction.StartShift ) {

                    id = GetGuardID(eventList[i].RawString);
                }
                else if ( eventList[i].Action == GuardAction.Asleep ) {
                    se = new SleepEvent {
                        ID = id,
                        Day = eventList[i].DT.Day,
                        Month = eventList[i].DT.Month
                    };
                    se.StartMin = eventList[i].DT.Minute;
                }
                else if ( eventList[i].Action == GuardAction.Awake ) {
                    se.StopMin = eventList[i].DT.Minute;
                    sleepAnalysis.Add(se);

                }
            }


            sleepAnalysis.Sort((tS, s) => s.TotalTimeAsleep.CompareTo(tS.TotalTimeAsleep));


            var summaryList = new Dictionary<int, SleepSummary>();

            for ( var i = 0; i < sleepAnalysis.Count; i++ ) {
                var sEvent = sleepAnalysis[i];

                if ( !summaryList.ContainsKey(sEvent.ID) ) {
                    summaryList.Add(sEvent.ID, new SleepSummary() {
                        SleepEvents = new List<SleepEvent>() { sEvent },
                        ID = sEvent.ID
                    });
                }
                else
                    summaryList[sEvent.ID].SleepEvents.Add(sEvent);
            }

            return summaryList;
        }

        private class SleepSummary
        {
            public int ID;
            public List<SleepEvent> SleepEvents;


            public int TotalTimeAsleep {
                get { return SleepEvents.Sum((a) => a.TotalTimeAsleep); }
            }



            public Tuple<int, int> MinMostAsleep()
            {
                var mins = new int[60];

                foreach (var se in SleepEvents)
                {
                    for (int i = se.StartMin; i < se.StopMin; i++)
                    {
                        mins[i]++;
                    }
                }

                var max = mins.Max(a => a);

                for (int i = 0; i < mins.Length; i++)
                {
                    if (mins[i] == max) return new Tuple<int, int>(i, mins[i]);
                }

                return null;
            }
        }

        private class SleepEvent
        {
            public int ID;
            public int Day;
            public int Month;
            public int StartMin;
            public int StopMin;

            public int TotalTimeAsleep {
                get { return StopMin - StartMin; }
            }
        }

        private int GetGuardID(string s)
        {
            return Convert.ToInt32(s.Split('#')[1].Split(' ')[0]);
        }

        private GuardAction GetGuardAction(string s)
        {
            switch (s.Split(']')[1][1])
            {
                case 'G':
                    return GuardAction.StartShift;
                    
                case 'w':
                    return GuardAction.Awake;

                case 'f':
                    return GuardAction.Asleep;

                default:
                    throw new Exception("Error! Can't find Guard Action in : " + s);
            }
        }

        public enum GuardAction
        {
            StartShift,
            Asleep,
            Awake
        }

        private class GuardEvent
        {
            public DateTime DT;
            public int GuardID;
            public GuardAction Action;
            public string RawString;
        }

        private DateTime GetDateInfo(string data)
        {
            var str = data.Split(']')[0];

            var m = Convert.ToInt32(str.Split('-')[1]);
            var d = Convert.ToInt32(str.Split('-')[2].Split(' ')[0]);
            var h = Convert.ToInt32(str.Split(':')[0].Split(' ')[1]);
            var min = Convert.ToInt32(str.Split(':')[1]);

            return new DateTime(1595, m,d,h, min, 0);
        }

    }
}