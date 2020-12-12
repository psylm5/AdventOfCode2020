using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_12
{
    internal class Program
    {
        #region Private Fields

        private static List<(char, int)> input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 12\day12.txt").Select(a => (a[0], int.Parse(a.Substring(1)))).ToList();

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        private static int Puzzle1()
        {
            var xDist = 0;
            var yDist = 0;

            var shipFacing = 90;

            foreach (var instruction in input)
            {
                switch (instruction.Item1)
                {
                    case ('N'):
                        xDist += instruction.Item2;
                        break;

                    case ('E'):
                        yDist += instruction.Item2;
                        break;

                    case ('S'):
                        xDist -= instruction.Item2;
                        break;

                    case ('W'):
                        yDist -= instruction.Item2;
                        break;

                    case ('L'):
                        shipFacing -= instruction.Item2;
                        break;

                    case ('R'):
                        shipFacing += instruction.Item2;
                        break;

                    case ('F'):

                        switch (mod(shipFacing, 360))
                        {
                            case (0):
                                xDist += instruction.Item2;
                                break;

                            case (90):
                                yDist += instruction.Item2;
                                break;

                            case (180):
                                xDist -= instruction.Item2;
                                break;

                            case (270):
                                yDist -= instruction.Item2;
                                break;

                            default:
                                throw new Exception("instr = F");
                        }
                        break;

                    default:
                        throw new Exception("invalid instr");
                }
            }

            return Math.Abs(xDist) + Math.Abs(yDist);
        }

        private static int Puzzle2()
        {
            var waypointPos = (1, 10);
            var shipPos = (0, 0);

            foreach (var instruction in input)
            {
                switch (instruction.Item1)
                {
                    case ('N'):
                        waypointPos.Item1 += instruction.Item2;
                        break;

                    case ('E'):
                        waypointPos.Item2 += instruction.Item2;
                        break;

                    case ('S'):
                        waypointPos.Item1 -= instruction.Item2;
                        break;

                    case ('W'):
                        waypointPos.Item2 -= instruction.Item2;
                        break;

                    case ('L'):
                        for (int i = 0; i < instruction.Item2 / 90; i++)
                            waypointPos = (waypointPos.Item2, (-1) * waypointPos.Item1);

                        break;

                    case ('R'):
                        for (int i = 0; i < instruction.Item2 / 90; i++)
                            waypointPos = ((-1) * waypointPos.Item2, waypointPos.Item1);
                        break;

                    case ('F'):
                        var multiplier = instruction.Item2;

                        shipPos.Item1 += (multiplier * waypointPos.Item1);
                        shipPos.Item2 += (multiplier * waypointPos.Item2);

                        break;

                    default:
                        throw new Exception("invalid instr");
                }
            }

            return Math.Abs(shipPos.Item1) + Math.Abs(shipPos.Item2);
        }

        #endregion Private Methods
    }
}