using System.Linq;

namespace AoC2020.Solutions
{
    public class Day09 : BaseDay
    {
        public override string SolveA()
        {
            var longs = lines.Select(long.Parse).ToArray();
            var result = Run(longs);
            return result != -1 ? result.ToString() : "no solution";
        }

        public override string SolveB()
        {
            var longs = lines.Select(long.Parse).ToArray();
            var invalidNumber = Run(longs);

            for (var i = 0; i < longs.Length; i++)
            {
                long sum = 0;
                for (var j = i; j < longs.Length; j++)
                {
                    sum += longs[j];
                    if (sum == invalidNumber)
                    {
                        return (longs[i..j].Min() + longs[i..j].Max()).ToString();
                    }

                    if (sum > invalidNumber)
                    {
                        break;
                    }
                }
            }

            return "no solution";
        }

        private static long Run(long[] longs)
        {
            for (var i = 25; i < longs.Length; i++)
            {
                var found = false;
                for (var j = i - 1; j >= i - 24; j--)
                {
                    for (var k = j - 1; k >= i - 25; k--)
                    {
                        if (longs[j] + longs[k] == longs[i])
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

                if (!found)
                {
                    return longs[i];
                }
            }

            return -1;
        }
    }
}