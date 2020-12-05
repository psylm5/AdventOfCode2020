using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_5
{
    internal class Program
    {
        #region Private Fields

        private static List<int> IDs;
        private static List<string> input;

        private static List<Tuple<int, int>> seats;

        #endregion Private Fields

        //Row: 7 digit
        //F =  0
        //B = 1

        //Column: 3 digit
        //L = 0
        //R = 1

        //ID = Row * 8 + Column

        #region Private Methods

        private static void Main(string[] args)
        {
            input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 5\Day5.txt").ToList();

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            IDs = input.Select(a =>
            {
                string row = String.Concat(a.Substring(0, 7).Select(a => a == 'F' ? '0' : '1'));
                string column = String.Concat(a.Substring(7, 3).Select(a => a == 'L' ? '0' : '1'));

                return (Convert.ToInt32(row, 2) * 8) + Convert.ToInt32(column, 2);
            }).ToList();

            return IDs.Max();
        }

        private static int Puzzle2()
        {
            seats = input.Select(a =>
            {
                int row = Convert.ToInt32(String.Concat(a.Substring(0, 7).Select(a => a == 'F' ? '0' : '1')), 2);
                int column = Convert.ToInt32(String.Concat(a.Substring(7, 3).Select(a => a == 'L' ? '0' : '1')), 2);

                return new Tuple<int, int>(row, column);
            }).ToList();

            foreach (var seat in seats)
            {
                int one = (seat.Item1 * 8) + seat.Item2;
                var two = (seat.Item1 * 8) + seat.Item2 + 1;
                int three = (seat.Item1 * 8) + seat.Item2 + 2;

                if (IDs.Contains(one) && !IDs.Contains(two) && IDs.Contains(three))
                    return two;
            }
            return 0;
        }

        #endregion Private Methods
    }
}