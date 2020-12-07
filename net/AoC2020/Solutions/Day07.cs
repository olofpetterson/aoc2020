using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day07 : BaseDay
    {
        public override string SolveA()
        {
            var dict = CreateData();
            return dict.Keys.Count(key => key != "shiny gold" && Run(dict, key)).ToString();
        }

        private static bool Run(Dictionary<string, List<(int, string)>> dict, string key)
        {
            return key == "shiny gold" || dict[key].Any(bag => Run(dict, bag.Item2));
        }

        private static int Run2(Dictionary<string, List<(int, string)>> dict, string key)
        {
            var bags = key != "shiny gold" ? 1 : 0;
            return dict[key].Aggregate(bags, (sum, bag) => sum + bag.Item1 * Run2(dict, bag.Item2));
        }

        public override string SolveB()
        {
            return Run2(CreateData(), "shiny gold").ToString();
        }

        private Dictionary<string, List<(int, string)>> CreateData()
        {
            var dict = new Dictionary<string, List<(int, string)>>();
            foreach (var line in lines)
            {
                var splits = line.Split(" contain ");
                var bagColour = string.Join(" ", splits[0].Split(" ").Take(2));
                var containSplit = splits[1].Split(", ");
                dict[bagColour] = new List<(int,string)>();
                foreach (var colour in containSplit)
                {
                    if (colour == "no other bags.") continue;
                    var c2 = string.Join(" ", colour[2..].Split(" ").Take(2));
                    dict[bagColour].Add((int.Parse(colour[0] + ""), c2));
                }
            }

            return dict;
        }
    }
}