using System.Linq;

namespace AoC2020.Solutions
{
    public class Day01 : BaseDay
    {
        public override string SolveA()
        {
            var integers = lines.Select(int.Parse).ToList();

            for (var i = 0; i < integers.Count; i++)
            {
                for (var j = i + 1; j < integers.Count; j++)
                {
                    if (integers[i] + integers[j] == 2020)
                    {
                        return (integers[i] * integers[j]).ToString();
                    }
                }
            }

            return "no solution";
        }

        public override string SolveB()
        {
            var integers = lines.Select(int.Parse).ToList();

            for (var i = 0; i < integers.Count; i++)
            {
                for (var j = i + 1; j < integers.Count; j++)
                {
                    if (integers[i] + integers[j] > 2020)
                        continue;
                    for (var k = j + 1; k < integers.Count; k++)
                    {
                        if (integers[i] + integers[j] + integers[k] == 2020)
                        {
                            return (integers[i] * integers[j] * integers[k]).ToString();
                        }
                    }
                }
            }

            return "no solution";
        }
    }
}