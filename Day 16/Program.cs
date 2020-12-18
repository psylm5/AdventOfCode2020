using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_16
{
    internal class Program
    {
        #region Private Fields

        private static List<string> input = File.ReadAllLines(@"D:\AdventOfCode\AdventOfCode\Day 16\day16.txt").ToList();

        private static List<int> MyTicket = new List<int>();
        private static List<List<int>> NearbyTickets = new List<List<int>>();
        private static Dictionary<string, List<int>> RulesPart1 = new Dictionary<string, List<int>>();
        private static Dictionary<string, List<int>> RulesPart2 = new Dictionary<string, List<int>>();
        private static List<List<int>> ValidNearbyTickets = new List<List<int>>();

        #endregion Private Fields

        #region Private Methods

        private static bool CheckRanges(int num, List<int> ranges)
        {
            var first = num >= ranges[0] && num <= ranges[1];
            var second = num >= ranges[2] && num <= ranges[3];

            return first || second;
        }

        private static bool CheckRanges2(int num, List<int> ranges)
        {
            var lower = num < ranges[0];
            var higher = num > ranges[3];

            return !(higher || lower);
        }

        private static List<int> GetRange(string min, string max)
        {
            var minNum = int.Parse(min);
            var maxNum = int.Parse(max);

            var range = new List<int>();

            for (int i = minNum; i <= maxNum; i++)
                range.Add(i);

            return range;
        }

        private static void Main(string[] args)
        {
            ParseInput();

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static string[] NarrowDown(List<List<string>> rules)
        {
            var output = new string[20];

            while (!rules.All(a => a.Count < 2))
            {
                var singles = rules.Where(a => a.Count == 1).SelectMany(x => x);

                foreach (var single in singles)
                {
                    foreach (var col in rules)
                    {
                        if (col.Count < 2)
                            continue;
                        col.Remove(single);
                    }
                }
            }

            return output;
        }

        private static void ParseInput()
        {
            var rules = input.GetRange(0, 20);

            var myTicket = input[22];

            var nearby = input.GetRange(25, input.Count - 25);

            //rules
            foreach (var rule in rules)
            {
                var field = rule.Substring(0, rule.IndexOf(':'));

                var ranges = rule.Substring(rule.IndexOf(':') + 2, rule.Length - (rule.IndexOf(':') + 2)).Split(" or ").Select(a => a.Split('-')).ToList();

                var validNumbers = new List<int>();

                foreach (var range in ranges)
                    validNumbers.AddRange(GetRange(range[0], range[1]));

                RulesPart1.Add(field, validNumbers);
                RulesPart2.Add(field, ranges.SelectMany(a => a).Select(a => int.Parse(a)).ToList());
            }

            //my ticket
            MyTicket = myTicket.Split(',').Select(a => int.Parse(a)).ToList();

            //nearby
            foreach (var nearbyTicket in nearby)
            {
                NearbyTickets.Add(nearbyTicket.Split(',').Select(a => int.Parse(a)).ToList());
            }
        }

        private static int Puzzle1()
        {
            var allValidNumbers = RulesPart1.Select(a => a.Value).SelectMany(a => a).Distinct().ToList();

            var errorRate = 0;

            foreach (var nearbyTicket in NearbyTickets)
            {
                var invalidNumbers = nearbyTicket.Where(a => !allValidNumbers.Contains(a));

                errorRate += invalidNumbers.Sum();
                if (invalidNumbers.Sum() == 0)
                    ValidNearbyTickets.Add(nearbyTicket);
            }

            return errorRate;
        }

        private static int Puzzle2()
        {
            /*var transposed = NearbyTickets.SelectMany(inner => inner.Select((item, index) => new { item, index }))
                                          .GroupBy(i => i.index, i => i.item)
                                          .Select(g => g.ToList())
                                          .ToList();

            var exampleRules = new Dictionary<string, List<int>> { { "class", new List<int> { 0, 1, 4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19 } },
                                                                   { "row", new List<int> { 0, 1, 2,3,4,5,8,9,10,11,12,13,14,15,16,17,18,19 } } ,
                                                                   { "seat", new List<int> { 0,1, 2,3,4,5,6,7,8,9,10,11,12,13,16,17,18,19 } }};

            var exampleNearby = new List<List<int>> { {new List<int>{3,9,18 } },
                                                      {new List<int>{15,1,5 } },
                                                      {new List<int>{5,14,9 } } };

            var exampleTransposed = exampleNearby.SelectMany(inner => inner.Select((item, index) => new { item, index }))
                                          .GroupBy(i => i.index, i => i.item)
                                          .Select(g => g.ToList())
                                          .ToList();

            foreach (var column in exampleTransposed)
            {
                var rule = exampleRules.Where(a => column.All(b => a.Value.Contains(b)));
            }

            foreach (var column in transposed)
            {
                var rule = RulesPart2.Where(a => column.All(b => CheckRanges(b, a.Value)));
            }*/

            var potentialRules = new List<List<string>>();

            for (int i = 0; i < 20; i++)
            {
                var column = ValidNearbyTickets.Select(a => a[i]).ToList();
                var rules = RulesPart2.Where(a => column.All(b => CheckRanges(b, a.Value))).Select(a => a.Key).ToList();
                potentialRules.Add(rules);
            }

            var abc = NarrowDown(potentialRules);

            return 0;
        }

        #endregion Private Methods
    }
}