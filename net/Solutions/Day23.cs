using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day23 : BaseDay
    {
        public override string SolveA()
        {
            var result = "";

            var deck = PlayGame(GetInput(), 100);
            var current = 1;
            for (var i = 1; i < deck.Count; i++)
            {
                result += (current = deck[current]);
            }

            return result;
        }

        public override string SolveB()
        {
            var deck = PlayGame(GetInput().Concat(Enumerable.Range(10, 999991)).ToArray(), 10000000);
            return (deck[1] * (long)deck[deck[1]]).ToString();
        }

        private Dictionary<int, int> PlayGame(int[] input, int turns)
        {
            var deck = new Dictionary<int, int>();
            for (var i = 0; i < input.Length; i++)
            {
                deck[input[i]] = input[(i + 1) % input.Length];
            }

            var max = input.Length;
            var min = 1;
            var current = input[0];
            for (var i = 0; i < turns; i++)
            {
                var selected = new[]
                {
                    deck[current],
                    deck[deck[current]],
                    deck[deck[deck[current]]]
                };
                var destination = current - 1;
                while (selected.Contains(destination) || destination < min)
                {
                    destination--;
                    if (destination < min)
                        destination = max;
                }

                deck[current] = deck[selected[2]];
                deck[selected[0]] = selected[1];
                deck[selected[1]] = selected[2];
                deck[selected[2]] = deck[destination];
                deck[destination] = selected[0];

                current = deck[current];
            }

            return deck;
        }

        private int[] GetInput()
            => lines[0].Select(x => int.Parse(x + "")).ToArray();
    }
}