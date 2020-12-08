using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_8
{
    internal class Program
    {
        #region Private Fields

        private static List<Tuple<string, int, bool>> input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 8\day8.txt").Select(a => new Tuple<string, int, bool>(
                                                                                                    a.Substring(0, a.IndexOf(' ')),
                                                                                                    int.Parse(a.Substring(a.IndexOf(' '))),
                                                                                                    false)).ToList();

        #endregion Private Fields

        #region Private Methods

        private static bool Execute(List<Tuple<string, int, bool>> program, out int PC, out int acc, out List<int> stack)
        {
            stack = new List<int>();

            PC = 0;
            acc = 0;

            while (true)
            {
                try
                {
                    if (PC == program.Count())
                        return true;

                    var op = program[PC];

                    if (op.Item3 == true)
                        return false;

                    program[PC] = new Tuple<string, int, bool>(op.Item1, op.Item2, true);

                    stack.Add(PC);

                    switch (op.Item1)
                    {
                        case "acc":
                            acc += op.Item2;
                            PC++;
                            break;

                        case "jmp":
                            PC += op.Item2;
                            break;

                        case "nop":
                            PC++;
                            break;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            Reset();
            Execute(input, out int pc, out int acc, out List<int> _);

            return acc;
        }

        private static int Puzzle2()
        {
            Reset();

            if (Execute(input, out int _, out int acc, out List<int> stack))
                return acc;

            Reset();

            foreach (var line in stack)
            {
                var prog = input.Where(a => true).ToList();

                var op = prog[line];

                switch (op.Item1)
                {
                    case "acc":
                        continue;
                    case "nop":
                        prog[line] = new Tuple<string, int, bool>("jmp", prog[line].Item2, false);
                        break;

                    case "jmp":
                        prog[line] = new Tuple<string, int, bool>("nop", prog[line].Item2, false);
                        break;
                }

                if (Execute(prog, out int _, out acc, out List<int> _))
                    return acc;
            }

            return 0;
        }

        private static void Reset()
        {
            input = input.Select(a => new Tuple<string, int, bool>(a.Item1, a.Item2, false)).ToList();
        }

        #endregion Private Methods
    }
}