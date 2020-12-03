using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    internal class Program
    {
        #region Private Fields

        private static readonly int Sum = 2020;

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            var inputStrings = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 1\Day1.txt").ToList();
            var input = inputStrings.Select(a => int.Parse(a));

            foreach (int i in input)
            {
                var diff = Sum - i;
                if (input.Contains(diff))
                    return diff * i;
            }

            return 0;
        }

        private static int Puzzle2()
        {
            var inputStrings = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 1\Day1.txt").ToList();
            var input = inputStrings.Select(a => int.Parse(a));

            foreach (int i in input)
            {
                var diff = Sum - i;
                foreach (int j in input)
                {
                    var diff2 = diff - j;

                    if (input.Contains(diff2))
                        return diff2 * i * j;
                }
            }
            return 0;
        }

        #endregion Private Methods
    }
}