using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018 {
    public class Day6 : Day {


        List<NodeCoords> nodes = new List<NodeCoords>();

        public override void Problem1()
        {
            var input = Helpers.ReadFileToSpreadsheet<int>("Day6_P1.txt", ',');

            var edges = new Coords[2];
            
            for (var i = 0; i < input.Count; i++)
                nodes.Add(new NodeCoords(input[i][0], input[i][1]));
            
            
            edges[0].X = nodes.Min(a => a.coords.X);
            edges[1].X = nodes.Max(a => a.coords.X);
            edges[0].Y = nodes.Min(a => a.coords.Y);
            edges[1].Y = nodes.Max(a => a.coords.Y);
            
            var map = new MappedCoords[edges[1].X + 1, edges[1].Y + 1];
            
            for (var i = 0; i < nodes.Count; i++)
            {
                //for each node, score all mapLocations.  
                var node = nodes[i];

                map[node.coords.X, node.coords.Y] = new MappedCoords(i);

                node.IsInfinite = IsOnEdge(ref edges, node.coords);

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        var location = map[x, y];
                        
                        var newDist = GetDistance(x, y, node);

                        if (location == null)
                        {
                            //first pass, all will be null.
                            map[x,y] = new MappedCoords(i, newDist);
                        }
                        else
                        {
                            if (location.Distance > newDist)
                            {

                                //if current distance is greater than ours
                                location.Distance = newDist;
                                location.NodeID = i;
                            }
                            else if (location.Distance == newDist)
                            {
                                //if current distance is equal to ours. 
                                // -> cancel out this node. 
                                location.NodeID = -1;
                            }
                        }
                    }
                }
            }

            for ( int y = 0; y < map.GetLength(1); y++ ) {
                for ( int x = 0; x < map.GetLength(0); x++ ) {
                    var location = map[x, y];

                    if ( location.NodeID >= 0 && !nodes[location.NodeID].IsInfinite )
                        nodes[location.NodeID].IsInfinite = IsOnEdge(ref edges, x, y);


                    if ( map[x, y].NodeID >= 0 && !nodes[map[x, y].NodeID].IsInfinite )
                        nodes[map[x, y].NodeID].SecondScore++;
                }
            }

            //var debugStr = "";
            //var point    = true;
            //for ( int y = 0; y < map.GetLength(1); y++ ) {
            //    for ( int x = 0; x < map.GetLength(0); x++ ) {

            //        var n = map[x, y].NodeID.ToString();

            //        if ( map[x, y].NodeID != -1 && nodes[map[x, y].NodeID].IsInfinite )
            //            n = "-1";

            //        debugStr += n + ",";
            //    }

            //    debugStr += "\n";
            //}

            //nodes.Sort((a, b) => a.SecondScore.CompareTo(b.SecondScore));
            //Console.WriteLine("Scores: \n\n");
            //for ( int i = 0; i < nodes.Count; i++ ) {
            //    Console.WriteLine("node: " + (nodes[i].IsInfinite ? "!" : "") + i + ": " + nodes[i].coords + "\t" + nodes[i].SecondScore);
            //}

            Print(1, nodes.Max(a => a.SecondScore), 3420);

        }

        private bool IsOnEdge(ref Coords[] edges, Coords c)
        {
            return IsOnEdge(ref edges, c.X, c.Y);
        }

        private bool IsOnEdge(ref Coords[] edges, int x, int y) {
            return ((x == edges[0].X || x == edges[1].X) || (y == edges[0].Y || y == edges[1].Y));
        }


        private struct Coords
        {
            public int X;
            public int Y;

            public override string ToString()
            {
                return "(" + X + ", " + Y + ")";
            }
        }

        private class NodeCoords
        {
            public Coords coords;
            public bool IsInfinite;
            public int SecondScore;

            public NodeCoords(int x, int y)
            {
                coords = new Coords { X = x, Y = y };
            }
        }

        private class MappedCoords
        {
            public int NodeID;
            public int Distance = -1;
            public int NewDist;

            public MappedCoords(int nodeID, int distance)
            {
                Distance = distance;
                NodeID = nodeID;
            }

            public MappedCoords(int nodeID) {
                NodeID   = nodeID;
            }

        }

        private int GetDistance(int fromX, int fromY, NodeCoords to)
        {
            return Math.Abs(fromX - to.coords.X) + Math.Abs(fromY - to.coords.Y);

        }


        //Wrong answers:
        //1583
        //2112
        //151773

        public override void Problem2()
        {

            var input = Helpers.ReadFileToSpreadsheet<int>("Day6_P1.txt", ',');

            var edges = new Coords[2];

            nodes.Clear();
            for ( var i = 0; i < input.Count; i++ )
                nodes.Add(new NodeCoords(input[i][0], input[i][1]));

            edges[0].X = nodes.Min(a => a.coords.X);
            edges[1].X = nodes.Max(a => a.coords.X);
            edges[0].Y = nodes.Min(a => a.coords.Y);
            edges[1].Y = nodes.Max(a => a.coords.Y);

            var map = new MappedCoords[edges[1].X + 1, edges[1].Y + 1];
            
            for ( int y = 0; y < map.GetLength(1); y++ ) {
                for (int x = 0; x < map.GetLength(0); x++) {
                    for (var i = 0; i < nodes.Count; i++) {

                        //for each node, score all mapLocations.  
                        var node = nodes[i];

                        //if (node.IsInfinite)
                        //    continue;

                        var location = map[x, y];

                        if (location == null)
                            location = map[x, y] = new MappedCoords(i);
                        
                        location.NewDist += GetDistance(x, y, node);
                    }
                }
            }

            //var debugStr = "";
            var score = 0;
            for ( int y = 0; y < map.GetLength(1); y++ ) {
                for ( int x = 0; x < map.GetLength(0); x++ ) {

                    var location = map[x, y];

                    if (location.NewDist >= 0 && location.NewDist < 10000)
                        score++;
                    else
                        location.NewDist = -1;

                    //var n = map[x, y].NewDist.ToString();
                    //if (nodes.Find(a =>  !a.IsInfinite && a.coords.Y == y && a.coords.X == x) != null)
                    //    n = "-50";
                    //debugStr += n + ",";
                }

               //debugStr += "\n";
            }

            Print(2, score, 46667);
        }
    }
}