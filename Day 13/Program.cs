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