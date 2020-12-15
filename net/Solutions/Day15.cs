using System.Linq;

namespace AoC2020.Solutions
{
    public class Day15 : BaseDay
    {
        public override string SolveA()
        {
            return Run(2020).ToString();
        }

        public override string SolveB()
        {
            return Run(30000000).ToString();
        }

        private int Run(int limit)
        {
            var numbers = lines[0].Split(",").Select((number, index) => (index + 1,  int.Parse(number))).ToDictionary(number => number.Item2, number => (number.Item1, 0));
            var count = numbers.Count;

            var lastNumber = numbers.Keys.Last();
            while (count++ < limit)
            {
                if (numbers.TryGetValue(lastNumber, out var value) && value.Item2 > 0)
                {
                    AddNumber(value.Item1 - value.Item2, count);
                }
                else
                {
                    AddNumber(0, count);
                }
            }

            void AddNumber(int number, int index)
            {
                if (numbers.TryGetValue(number, out var value))
                {
                    numbers[number] = (index, value.Item1);
                }
                else
                {
                    numbers[number] = (index, 0);
                }

                lastNumber = number;
            }

            return lastNumber;
        }
    }
}