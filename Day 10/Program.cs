using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_10
{
    internal class Program
    {
        #region Private Fields

        private static List<int> differences;
        private static List<int> input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 10\day10.txt").Select(a => int.Parse(a)).ToList();

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            input.Sort();

            input.Insert(0, 0);

            differences = new List<int>();

            for (int i = 0; i < input.Count - 1; i++)
                differences.Add(input[i + 1] - input[i]);

            differences.Add(3);

            var threes = differences.Where(a => a == 3).Count();
            var ones = differences.Where(a => a == 1).Count();

            return threes * ones;
        }

        private static long Puzzle2()
        {
            var diffs = string.Concat(differences);
            var ones = diffs.Split('3').Where(a => a != "").ToList();

            long total = 1;

            foreach (var one in ones)
            {
                switch (one)
                {
                    case "1":
                        break;

                    case "11":
                        total *= 2;
                        break;

                    case "111":
                        total *= 4;
                        break;

                    case "1111":
                        total *= 7;
                        break;
                }
            }
            return total;
        }

        #endregion Private Methods
    }
}