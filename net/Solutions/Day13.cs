using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day13 : BaseDay
    {
        public override string SolveA()
        {
            var start = int.Parse(lines[0]);
            var buses = lines[1].Split(",").Where(x => x != "x").Select(int.Parse);
            var min = int.MaxValue;
            var minBus = 0;

            foreach (var bus in buses)
            {
                var before = start / bus;
                var diff = (before + 1) * bus - start;
                if (diff < min)
                {
                    min = diff;
                    minBus = bus;
                }
            }

            return (minBus * min).ToString();
        }

        public override string SolveB()
        {
            var buses = new List<(long cycle, long offset)>();
            lines[1].Split(",").Aggregate(0, (index, bus) =>
            {
                if (bus != "x")
                {
                    buses.Add((long.Parse(bus), index));
                }

                return index + 1;
            });

            var (departure, _) = buses.OrderByDescending(x => x.cycle).Aggregate((departure: 0L, buses[0].cycle), (input, bus) =>
            {
                while ((input.departure + bus.offset) % bus.cycle != 0)
                {
                    input.departure += input.cycle;
                }

                return (input.departure, input.cycle * bus.cycle);
            });
            return departure.ToString();
        }
    }
}