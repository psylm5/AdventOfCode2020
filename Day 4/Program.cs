using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
    internal class Program
    {
        #region Private Fields

        private static readonly string[] eyeColours = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        private static readonly string[] fields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        private static List<List<string>> inputPassports;

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            var input = File.ReadAllText(@"D:\AdventOfCode\AdventOfCode\Day 4\Day4.txt").Split("\n\n").ToList();
            inputPassports = input.Select(a => a.Split('\n', ' ').ToList()).ToList();

            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        private static int Puzzle1()
        {
            var validCount = 0;

            foreach (var passport in inputPassports)
            {
                var entries = passport.Select(a => a.Substring(0, 3));

                var valid = true;

                foreach (var field in fields)
                {
                    if (field == "cid")
                        continue;
                    else if (!entries.Contains(field))
                        valid = false;
                }

                if (valid)
                    validCount++;
            }

            return validCount;
        }

        private static int Puzzle2()
        {
            var validCount = 0;

            List<List<string>> invalids = new List<List<string>>();

            foreach (var passport in inputPassports)
            {
                passport.RemoveAll(a => a == "");
                var entries = passport.Select(a => new KeyValuePair<string, string>(a.Substring(0, 3), a.Substring(4, a.Length - 4)));

                var valid = true;
                foreach (var field in fields)
                {
                    try
                    {
                        //catches where field missing
                        var entry = entries.Where(a => a.Key == field).ToList()[0];

                        switch (entry.Key)
                        {
                            case "byr":
                                var byr = int.Parse(entry.Value);
                                if (entry.Value.Length != 4 || byr <= 1919 || byr >= 2003)
                                    valid = false;
                                break;

                            case "iyr":
                                var iyr = int.Parse(entry.Value);
                                if (entry.Value.Length != 4 || iyr <= 2009 || iyr >= 2021)
                                    valid = false;
                                break;

                            case "eyr":
                                var eyr = int.Parse(entry.Value);
                                if (entry.Value.Length != 4 || eyr <= 2019 || eyr >= 2031)
                                    valid = false;
                                break;

                            case "hgt":
                                var height = int.Parse(entry.Value.Substring(0, entry.Value.Length - 2));
                                var unit = entry.Value.Substring(entry.Value.Length - 2, 2);
                                if (unit == "cm")
                                {
                                    if (height <= 149 || height >= 194)
                                        valid = false;
                                }
                                else if (unit == "in")
                                {
                                    if (height <= 58 || height >= 77)
                                        valid = false;
                                }
                                else
                                {
                                    valid = false;
                                }
                                break;

                            case "hcl":
                                var hcl = entry.Value.Substring(1);

                                var allowedChars = "abcdef0123456789";

                                if (entry.Value[0] != '#')
                                    valid = false;
                                if (hcl.Length != 6)
                                    valid = false;

                                foreach (var c in hcl)
                                    if (!allowedChars.Contains(c))
                                        valid = false;

                                break;

                            case "ecl":
                                var colour = entry.Value;

                                if (!eyeColours.Contains(colour))
                                    valid = false;

                                break;

                            case "pid":
                                var pid = int.Parse(entry.Value);
                                if (entry.Value.Length != 9)
                                    valid = false;
                                break;

                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (field != "cid")
                            valid = false;
                    }
                }

                if (valid)
                    validCount++;
                else
                    invalids.Add(passport);
            }

            return validCount;
        }

        #endregion Private Methods
    }
}