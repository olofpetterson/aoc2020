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
            var numbers = lines[0].Split(",")
                .Select((number, index) => (index: index + 1, number: int.Parse(number)))
                .ToDictionary(number => number.number, number => (number.index, previousIndex: 0));
            var lastNumber = numbers.Keys.Last();
            
            for(var i = numbers.Count + 1; i <= limit; i++)
            {
                if (numbers.TryGetValue(lastNumber, out var value) && value.previousIndex > 0)
                {
                    AddNumber(value.index - value.previousIndex, i);
                }
                else
                {
                    AddNumber(0, i);
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