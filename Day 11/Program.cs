using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_11
{
    internal class Program
    {
        #region Private Fields

        private static List<string> input;

        #endregion Private Fields

        #region Private Methods

        private static char? FindChair(List<string> inputArrangement, int i, int j, Func<int, int> iFun, Func<int, int> jFun)
        {
            while (true)
            {
                i = iFun(i);
                j = jFun(j);

                try
                {
                    var currChar = inputArrangement[i][j];
                    if (currChar != '.')
                    {
                        return currChar;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        private static List<char> GetAdjacent(List<string> inputArrangement, int i, int j)
        {
            var output = new List<char>();

            Func<int, int> minus = a => a - 1;
            Func<int, int> id = a => a;
            Func<int, int> plus = a => a + 1;

            output.Add(FindChair(inputArrangement, i, j, minus, minus) ?? ' ');
            output.Add(FindChair(inputArrangement, i, j, minus, id) ?? ' ');
            output.Add(FindChair(inputArrangement, i, j, id, minus) ?? ' ');
            output.Add(FindChair(inputArrangement, i, j, minus, plus) ?? ' ');
            output.Add(FindChair(inputArrangement, i, j, plus, minus) ?? ' ');
            output.Add(FindChair(inputArrangement, i, j, plus, id) ?? ' ');
            output.Add(FindChair(inputArrangement, i, j, id, plus) ?? ' ');
            output.Add(FindChair(inputArrangement, i, j, plus, plus) ?? ' ');

            output.RemoveAll(c => c == ' ');

            return output;
        }

        private static void Main(string[] args)
        {
            input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 11\day11.txt").ToList();

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static List<string> Part1(List<string> inputArrangement)
        {
            List<string> current = inputArrangement;
            List<string> next = new List<string>();

            for (int i = 0; i < current.Count; i++)
            {
                var oldRow = current[i];
                var newRow = "";

                for (int j = 0; j < oldRow.Length; j++)
                {
                    var seat = current[i][j];

                    if (seat == '.')
                    {
                        newRow += '.';
                        continue;
                    }

                    var a = new List<int> { i - 1, i, i + 1 };
                    var b = new List<int> { j - 1, j, j + 1 };
                    var adjacent = a.SelectMany(l => b, (l, r) => new Tuple<int, int>(l, r)).Where(a => a.Item1 >= 0 &&
                                                                                                        a.Item1 < current.Count &&
                                                                                                        a.Item2 >= 0 &&
                                                                                                        a.Item2 < oldRow.Length && !(a.Item1 == i && a.Item2 == j)).
                                                                                                        Select(b => current[b.Item1][b.Item2]).ToList();

                    if (seat == 'L')
                    {
                        newRow += adjacent.Count(a => a == '#') == 0 ? '#' : 'L';
                    }
                    else if (seat == '#')
                    {
                        newRow += adjacent.Count(a => a == '#') > 3 ? 'L' : '#';
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                next.Add(newRow);
            }

            return next;
        }

        private static List<string> Part2(List<string> inputArrangement)
        {
            List<string> current = inputArrangement;
            List<string> next = new List<string>();

            for (int i = 0; i < current.Count; i++)
            {
                var oldRow = current[i];
                var newRow = "";

                for (int j = 0; j < oldRow.Length; j++)
                {
                    double percentage = (double)((i + 1) * (j + 1)) / (double)(current.Count * oldRow.Length);

                    Console.WriteLine('\r' + percentage.ToString("#.######"));

                    var seat = current[i][j];

                    if (seat == '.')
                    {
                        newRow += '.';
                        continue;
                    }

                    var adjacent = GetAdjacent(current, i, j);

                    if (seat == 'L')
                    {
                        newRow += adjacent.Count(a => a == '#') == 0 ? '#' : 'L';
                    }
                    else if (seat == '#')
                    {
                        newRow += adjacent.Count(a => a == '#') > 4 ? 'L' : '#';
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                next.Add(newRow);
            }

            return next;
        }

        private static int Puzzle1()
        {
            var xd = Run(Part1);
            return xd;
        }

        private static int Puzzle2()
        {
            return Run(Part2);
        }

        private static int Run(Func<List<string>, List<string>> algo)
        {
            var prev = new List<string>();
            var next = input;

            while (!prev.SequenceEqual(next))
            {
                Console.WriteLine("ZERO PERCENT\n");

                prev = next;
                next = algo(prev);
            }

            return string.Concat(next).Count(c => c == '#');
        }

        #endregion Private Methods
    }
}