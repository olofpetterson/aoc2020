using System;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day06 : BaseDay
    {
        public override string SolveA()
        {
            var groups = data.Split(Environment.NewLine + Environment.NewLine);
            return groups.Sum(group => group.Where(c => c >= 97 && c <= 122).Distinct().Count()).ToString();
        }

        public override string SolveB()
        {
            var range = Enumerable.Range(97, 26).ToList();
            var groups = data.Split(Environment.NewLine + Environment.NewLine);
            return groups.Sum(group => range.Count(ch => group.Split(Environment.NewLine).All(line => line.Contains((char) ch)))).ToString();
        }
    }
}