using System.Linq;

namespace AoC2020.Solutions
{
    public class Day25 : BaseDay
    {
        public override string SolveA()
        {
            var keys = lines.Select(int.Parse).ToArray();
            var value = 1L;
            var loop = 0;
            while (value != keys[0])
            {
                loop++;
                value *= 7;
                value %= 20201227;
            }

            value = 1;
            for(var j = 0; j < loop; j++)
            {
                value *= keys[1];
                value %= 20201227;
            }

            return value.ToString();
        }

        public override string SolveB()
        {
            return "";
        }
    }
}