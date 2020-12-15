using System;
using System.Collections.Generic;

namespace Day_15
{
    internal class Program
    {
        #region Private Fields

        private static List<long> input = new List<long> { 1, 2, 16, 19, 18, 0 };

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            Console.WriteLine(Puzzle1().ToString());
            Console.WriteLine(Puzzle2().ToString());
        }

        /// <summary>
        /// Naive solution
        /// </summary>
        private static int PlayGame(List<int> start, int turns)
        {
            var game = start;
            game.Reverse();

            for (int i = game.Count; i < turns; i++)
            {
                var first = game.Count;
                var lastOccurance = game.IndexOf(game[0], 1);

                if (lastOccurance == -1)
                {
                    game.Insert(0, 0);
                    continue;
                }

                var second = game.Count - lastOccurance;

                game.Insert(0, first - second);
            }

            return game[0];
        }

        /// <summary>
        /// Solution using dictionary for speed after checking online
        /// </summary>
        private static long PlayGameFast(List<long> start, long turns)
        {
            //key = number
            //value = (most recent occurance, 2nd most recent occurance)
            var occurances = new Dictionary<long, (long, long)>();

            //add starting numbers
            for (int i = 0; i < start.Count; i++)
            {
                occurances.Add(start[i], (i, -1));
            }

            //rest of turns
            for (int i = start.Count; i < turns; i++)
            {
                var prev = start[i - 1];
                long valueToAdd = 0;

                occurances.TryGetValue(prev, out var value);

                //if value has never appeared add zero
                if (value.Item2 == -1)
                {
                    valueToAdd = 0;
                }
                //else add difference between last occurances
                else
                {
                    valueToAdd = occurances[prev].Item1 - occurances[prev].Item2;
                }

                //are we adding a number that already exists?
                var exists = occurances.TryGetValue(valueToAdd, out _);

                if (exists)
                    occurances[valueToAdd] = (i, occurances[valueToAdd].Item1);
                else
                    occurances.Add(valueToAdd, (i, -1));

                start.Add(valueToAdd);
            }

            return start[(int)turns - 1];
        }

        private static long Puzzle1()
        {
            var numbers = new List<long>(input);

            return PlayGameFast(numbers, 2020);
        }

        private static long Puzzle2()
        {
            var numbers = new List<long>(input);

            return PlayGameFast(numbers, 30000000);
        }

        #endregion Private Methods
    }
}