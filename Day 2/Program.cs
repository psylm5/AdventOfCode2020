using System;
using System.IO;
using System.Linq;

namespace Day2
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            var inputStrings = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 2\Day2.txt").ToList();

            var parsed = inputStrings.Select(a => new Tuple<int, int, char, string>(
                                                int.Parse(a.Substring(0, a.IndexOf('-'))),
                                                int.Parse(a.Substring(a.IndexOf('-') + 1, a.IndexOf(' ') - a.IndexOf('-'))),
                                                a[a.IndexOf(' ') + 1],
                                                a.Substring(a.IndexOf(':') + 2)));

            var validPWs = 0;

            foreach (var pw in parsed)
            {
                var occurances = pw.Item4.Count(a => a == pw.Item3);

                if (occurances >= pw.Item1 && occurances <= pw.Item2)
                    validPWs++;
            }

            return validPWs;
        }

        private static int Puzzle2()
        {
            var inputStrings = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 2\Day2.txt").ToList();

            var parsed = inputStrings.Select(a => new Tuple<int, int, char, string>(
                                                int.Parse(a.Substring(0, a.IndexOf('-'))),
                                                int.Parse(a.Substring(a.IndexOf('-') + 1, a.IndexOf(' ') - a.IndexOf('-'))),
                                                a[a.IndexOf(' ') + 1],
                                                a.Substring(a.IndexOf(':') + 2)));

            var validPWs = 0;

            foreach (var pw in parsed)
            {
                var pos1 = pw.Item4[pw.Item1 - 1] == pw.Item3;
                var pos2 = pw.Item4[pw.Item2 - 1] == pw.Item3;

                if (pos1 ^ pos2)
                    validPWs++;
            }

            return validPWs;
        }

        #endregion Private Methods
    }
}