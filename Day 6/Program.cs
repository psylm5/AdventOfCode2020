using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_6
{
    internal class Program
    {
        #region Private Fields

        private static List<string> input;

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            input = File.ReadAllText(@"D:\AdventOfCode\AdventOfCode\Day 6\Day6.txt").Split("\n\n").ToList();

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            return input.Select(a => string.Concat(a.Distinct()).Replace("\n", string.Empty).Count()).Sum();
        }

        private static int Puzzle2()
        {
            return input.Select(a =>
            {
                var people = a.Split('\n');

                IEnumerable<char> str = people[0];

                foreach (var person in people)
                {
                    if (person == "")
                        continue;

                    str = str.Intersect(person);
                }

                return str.Count();
            }).Sum();
        }

        #endregion Private Methods
    }
}