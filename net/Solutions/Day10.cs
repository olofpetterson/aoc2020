using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day10 : BaseDay
    {
        public override string SolveA()
        {
            var adapters = lines.Select(int.Parse).ToList();
            adapters.AddRange(new[] {0, adapters.Max() + 3});
            adapters.Sort();

            var ones = 0;
            var threes = 0;

            for (var i = 1; i < adapters.Count; i++)
            {
                if (adapters[i] - adapters[i - 1] == 1)
                    ones++;
                if (adapters[i] - adapters[i - 1] == 3)
                    threes++;
            }

            return (ones * threes).ToString();
        }

        public override string SolveB()
        {
            var adapters = lines.Select(int.Parse).ToList();
            adapters.AddRange(new[] {0, adapters.Max() + 3});
            adapters.Sort();
            adapters.Reverse();
            
            var connections = new Dictionary<int, long>();
            
            foreach(var adapter in adapters)
            {
                connections[adapter] = Math.Max(adapters.Where(x => x > adapter && x <= adapter + 3).Sum(x => connections[x]), 1);
            }

            return connections[0].ToString();
        }
    }
}