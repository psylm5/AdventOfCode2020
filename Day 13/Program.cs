using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_13
{
    internal class Program
    {
        #region Private Fields

        private static List<int> busses;
        private static List<string> busses2;
        private static int earliestDeparture;

        #endregion Private Fields

        #region Private Methods

        private static (long, long) FindNext(long start, long jump, long a, long offsetA)
        {
            var valid = new List<long>();

            long i = start;
            while (true)
            {
                if (valid.Count == 2)
                    return (valid[0], valid[1] - valid[0]);

                i += jump;

                if ((i + offsetA) % a == 0)
                {
                    valid.Add(i);
                }
            }
        }

        private static long IterativeSearch(List<(long, long)> busses)
        {
            long start = 0;
            long step = 1;

            for (int i = 0; i < busses.Count; i++)
            {
                var next = busses[i];

                (start, step) = FindNext(start, step, next.Item1, next.Item2);
            }

            return start;
        }

        private static void Main(string[] args)
        {
            var input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 13\day13.txt");

            earliestDeparture = int.Parse(input[0]);
            busses = input[1].Split(',').Where(a => a != "x").Select(a => int.Parse(a)).ToList();
            busses2 = input[1].Split(',').ToList();

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static double Puzzle1()
        {
            var bus = busses.Select(a => (a, Math.Ceiling((double)earliestDeparture / a) * a)).OrderBy(p => p.Item2).FirstOrDefault();

            return (bus.Item2 - earliestDeparture) * bus.Item1;
        }

        private static long Puzzle2()
        {
            var puzzle2Input = new List<(long, long)>();

            var offset = 0;

            foreach (var bus in busses2)
            {
                if (bus != "x")
                    puzzle2Input.Add((long.Parse(bus), offset));

                offset++;
            }

            return IterativeSearch(puzzle2Input);
        }

        /// <summary>
        /// takes many years to run
        /// </summary>
        private static long Puzzle2BruteForce()
        {
            long n = 100000000000000;

            long max = busses.Max();
            long maxIndex = busses2.IndexOf(max.ToString());

            while (n % max != 0)
                n++;

            n -= max;

            while (true)
            {
                n += max;

                long m = n - maxIndex;
                long o = m;

                foreach (var bus in busses2)
                {
                    if (bus != "x")
                    {
                        long id = int.Parse(bus);
                        if (o % id != 0)
                            break;
                    }

                    o++;
                }

                if (o - m == busses2.Count)
                    return m;
            }
        }

        #endregion Private Methods
    }
}