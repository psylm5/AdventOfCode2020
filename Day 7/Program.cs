using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_7
{
    internal class Program
    {
        #region Private Fields

        private static List<string> allowedBags = new List<string>();
        private static List<string> input;
        private static List<KeyValuePair<string, List<string>>> rules = new List<KeyValuePair<string, List<string>>>();

        #endregion Private Fields

        #region Private Methods

        private static void checkBags(string bagToCheck)
        {
            if (allowedBags.Contains(bagToCheck))
                return;

            allowedBags.Add(bagToCheck);

            var allowed = rules.Where(a => !string.IsNullOrEmpty(a.Value.FirstOrDefault(a => a.Contains(bagToCheck))));

            foreach (var bag in allowed)
                checkBags(bag.Key);
        }

        private static int CountBags(string initialBag)
        {
            try
            {
                var count = 1;

                var rule = rules.Where(a => a.Key.Contains(initialBag)).ToList()[0];

                foreach (var bag in rule.Value)
                {
                    var num = int.Parse(bag[0] + "");

                    var bagString = bag.Substring(2);

                    var index = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        index = bagString.IndexOf(" ", index) + 1;
                    }

                    var colour = bagString.Substring(0, index - 1);

                    count += num * CountBags(colour);
                }

                return count;
            }
            catch
            {
                return 1;
            }
        }

        private static void Main(string[] args)
        {
            input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 7\day7.txt").ToList();

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            foreach (var rule in input)
            {
                var index = 0;
                for (int i = 0; i < 2; i++)
                {
                    index = rule.IndexOf(" ", index) + 1;
                }

                var key = rule.Substring(0, index - 1);

                for (int i = 0; i < 2; i++)
                {
                    index = rule.IndexOf(" ", index) + 1;
                }

                var value = rule.Substring(index).Split(',').ToList();
                value = value.Select(a => a.Trim()).ToList();
                rules.Add(new KeyValuePair<string, List<string>>(key, value));
            }

            checkBags("shiny gold");

            return allowedBags.Count() - 1;
        }

        private static int Puzzle2()
        {
            return CountBags("shiny gold") - 1;
        }

        #endregion Private Methods
    }
}