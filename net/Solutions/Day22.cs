using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day22 : BaseDay
    {
        public override string SolveA()
        {
            var (deck1, deck2) = GetDecks();

            while (deck1.Any() && deck2.Any())
            {
                var cardOne = deck1[0];
                var cardTwo = deck2[0];
                deck1.RemoveAt(0);
                deck2.RemoveAt(0);
                if (cardOne > cardTwo)
                {
                    deck1.Add(cardOne);
                    deck1.Add(cardTwo);
                }
                else
                {
                    deck2.Add(cardTwo);
                    deck2.Add(cardOne);
                }
            }

            if (deck1.Any())
            {
                return deck1.Select((card, index) => card * (deck1.Count - index)).Sum().ToString();
            }

            return deck2.Select((card, index) => card * (deck2.Count - index)).Sum().ToString();
        }

        public override string SolveB()
        {
            var (deck1, deck2) = GetDecks();
            return PlayGame(deck1, deck2, 1).Item2.ToString();
        }

        private (List<int>, List<int>) GetDecks()
        {
            var blocks = data.Split(Environment.NewLine+Environment.NewLine);
            var deck1 = blocks[0].Split(Environment.NewLine)[1..].Select(int.Parse).ToList();
            var deck2 = blocks[1].Split(Environment.NewLine)[1..].Select(int.Parse).ToList();
            return (deck1, deck2);
        }

        private static (int, int) PlayGame(List<int> deck1, List<int> deck2, int depth)
        {
            var hash = new HashSet<string>();
            while (deck1.Any() && deck2.Any())
            {
                if (!hash.Add(string.Join(",", deck1) + ":" + string.Join(",", deck2)))
                {
                    return (1, deck1.Select((a, b) => a * (deck1.Count - b)).Sum());
                }

                var cardOne = deck1[0];
                var cardTwo = deck2[0];
                deck1.RemoveAt(0);
                deck2.RemoveAt(0);

                if (cardOne <= deck1.Count && cardTwo <= deck2.Count)
                {
                    var result = PlayGame(deck1.Take(cardOne).ToList(), deck2.Take(cardTwo).ToList(), depth + 1);
                    if (result.Item1 == 1)
                    {
                        deck1.Add(cardOne);
                        deck1.Add(cardTwo);
                    }
                    else
                    {                       
                        deck2.Add(cardTwo);
                        deck2.Add(cardOne);
                    }
                }
                else
                {
                    if (cardOne > cardTwo)
                    {
                        deck1.Add(cardOne);
                        deck1.Add(cardTwo);
                    }
                    else
                    {                       
                        deck2.Add(cardTwo);
                        deck2.Add(cardOne);
                    }
                }
            }

            if (deck1.Any())
            {
                return (1, deck1.Select((a, b) => a * (deck1.Count - b)).Sum());
            }

            return (2, deck2.Select((a, b) => a * (deck2.Count - b)).Sum());
        }
    }
}