using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_14
{
    internal class Program
    {
        #region Private Fields

        private static List<string> input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 14\day14.txt").ToList();

        #endregion Private Fields

        #region Private Methods

        private static long ApplyMask(string value, string mask)
        {
            var output = new string('0', mask.Length).ToCharArray();
            var charsToSkip = mask.Length - value.Length;

            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case '1':
                    case '0':
                        output[i] = mask[i];
                        break;

                    case 'X':
                        if (i - charsToSkip >= 0)
                            output[i] = value[i - charsToSkip];
                        break;
                }
            }

            return Convert.ToInt64(string.Concat(output), 2);
        }

        private static List<long> FloatingAddrToConcrete(string address)
        {
            var x = address.IndexOf('X');

            if (x == -1)
                return new List<long> { Convert.ToInt64(string.Concat(address), 2) };

            var output = new List<long>();

            var zero = address.ToCharArray();
            zero[x] = '0';
            output.AddRange(FloatingAddrToConcrete(string.Concat(zero)));

            var one = address.ToCharArray();
            one[x] = '1';
            output.AddRange(FloatingAddrToConcrete(string.Concat(one)));

            return output;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static long Puzzle1()
        {
            var mask = "";

            var mem = new Dictionary<int, long>();

            foreach (var instruction in input)
            {
                if (instruction.Substring(0, instruction.IndexOf(' ')) == "mask")
                {
                    mask = instruction.Substring(instruction.IndexOf('=') + 2);
                }
                else
                {
                    var open = instruction.IndexOf('[') + 1;
                    var close = instruction.IndexOf(']');

                    var addr = int.Parse(instruction.Substring(open, close - open));

                    string value = Convert.ToString(int.Parse(instruction.Substring(instruction.IndexOf('=') + 1)), 2);

                    try
                    {
                        mem.Add(addr, ApplyMask(value, mask));
                    }
                    catch
                    {
                        mem.Remove(addr);
                        mem.Add(addr, ApplyMask(value, mask));
                    }
                }
            }

            return mem.Sum(a => a.Value);
        }

        private static long Puzzle2()
        {
            var mask = "";

            var mem = new Dictionary<long, int>();

            foreach (var instruction in input)
            {
                if (instruction.Substring(0, instruction.IndexOf(' ')) == "mask")
                {
                    mask = instruction.Substring(instruction.IndexOf('=') + 2);
                }
                else
                {
                    var open = instruction.IndexOf('[') + 1;
                    var close = instruction.IndexOf(']');

                    string addr = Convert.ToString(int.Parse(instruction.Substring(open, close - open)), 2);

                    int value = int.Parse(instruction.Substring(instruction.IndexOf('=') + 1));

                    var addresses = TranslateAddress(mask, addr);

                    foreach (var address in addresses)
                    {
                        try
                        {
                            mem.Add(address, value);
                        }
                        catch (ArgumentException)
                        {
                            mem.Remove(address);
                            mem.Add(address, value);
                        }
                    }
                }
            }

            var values = new List<long>(mem.Select(a => (long)a.Value));

            return values.Sum();
        }

        private static List<long> TranslateAddress(string mask, string address)
        {
            var floatingAddress = new string('0', mask.Length).ToCharArray();
            var charsToSkip = mask.Length - address.Length;

            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case '0':
                        if (i - charsToSkip >= 0)
                            floatingAddress[i] = address[i - charsToSkip];
                        break;

                    case 'X':
                    case '1':
                        floatingAddress[i] = mask[i];
                        break;
                }
            }

            return FloatingAddrToConcrete(string.Concat(floatingAddress)); //Convert.ToInt64(string.Concat(output), 2);
        }

        #endregion Private Methods
    }
}