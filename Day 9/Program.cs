using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_9
{
    internal class Program
    {
        #region Private Fields

        private static List<long> input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 9\day9.txt").Select(a => long.Parse(a)).ToList();

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            var weakness = Puzzle1(25);

            Console.WriteLine(weakness.ToString());
            Console.WriteLine(Puzzle2(weakness).ToString());
        }

        private static long Puzzle1(int preambleLength)
        {
            for (int i = preambleLength, j = 0; i < input.Count(); i++, j++)
            {
                var preamble = input.GetRange(j, preambleLength);
                var value = input[i];

                var found = false;

                foreach (var num in preamble)
                {
                    var search = value - num;

                    if (search == num)
                        continue;

                    if (preamble.Contains(search))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    continue;
                else
                    return value;
            }

            return 0;
        }

        private static long Puzzle2(long weakness)
        {
            for (int i = 0; i < input.Count(); i++)
            {
                var j = i;
                var total = input[i];

                var smallest = Math.Min(input[i], input[i + 1]);
                var largest = Math.Max(input[i], input[i + 1]);

                while (true)
                {
                    j++;

                    total += input[j];

                    if (input[j] > largest)
                        largest = input[j];

                    if (input[j] < smallest)
                        smallest = input[j];

                    if (total == weakness)
                        return smallest + largest;

                    if (total > weakness)
                        break;
                }
            }

            return 0;
        }

        #endregion Private Methods
    }
}