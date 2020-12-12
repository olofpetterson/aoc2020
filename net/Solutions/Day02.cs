using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020.Solutions
{
    public class Day02 : BaseDay
    {
        public override string SolveA()
        {
            var validPasswords = 0;
            Parallel.ForEach(lines, line =>
            {
                var splits = line.Split(' ');
                var numbers = splits[0].Split('-').Select(int.Parse).ToArray();
                var validLetter = splits[1][0];
                var password = splits[2];

                var count = password.Count(letter => letter == validLetter);
                if (count >= numbers[0] && count <= numbers[1])
                {
                    Interlocked.Increment(ref validPasswords);
                }
            });

            return validPasswords.ToString();
        }

        public override string SolveB()
        {
            var validPasswords = 0;
            Parallel.ForEach(lines, line =>
            {
                var splits = line.Split(' ');
                var numbers = splits[0].Split('-').Select(int.Parse).ToArray();
                var validChar = splits[1][0];

                if ((splits[2][numbers[0] - 1] == validChar) != (splits[2][numbers[1] - 1] == validChar))
                {
                    Interlocked.Increment(ref validPasswords);
                }
            });

            return validPasswords.ToString();
        }
    }
}