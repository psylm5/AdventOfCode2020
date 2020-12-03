using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_3
{
    internal class Program
    {
        #region Private Fields

        private static int depth;

        private static List<string> inputStrings;

        private static int width;

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            inputStrings = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 3\Day3.txt").ToList();

            width = inputStrings[0].Length;
            depth = inputStrings.Count;

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            return Traverse(3, 1);
        }

        private static int Puzzle2()
        {
            return Traverse(1, 1) * Traverse(3, 1) * Traverse(5, 1) * Traverse(7, 1) * Traverse(1, 2);
        }

        private static int Traverse(int xStep, int yStep)
        {
            var x = 0;
            var y = 0;

            var collisions = 0;

            while (true)
            {
                x += xStep;
                y += yStep;

                if (x >= width)
                    x -= width;

                if (y >= depth)
                    return collisions;

                if (inputStrings[y][x] == '#')
                    collisions++;
            }
        }

        #endregion Private Methods
    }
}