using System;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day05 : BaseDay
    {
        public override string SolveA()
        {
            return lines.Max(line => Convert.ToInt16(line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2)).ToString();
        }

        public override string SolveB()
        {
            var ids = lines.Select(line => Convert.ToInt16(line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2)).ToList();
            ids.Sort();

            for (var i = 0; i < ids.Count - 1; i++)
            {
                if (ids[i] != ids[i + 1] - 1)
                {
                    return (ids[i] + 1).ToString();
                }
            }

            return "no solution";
        }
    }
}