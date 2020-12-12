using System.Linq;

namespace AoC2020.Solutions
{
    public class Day03 : BaseDay
    {
        public override string SolveA()
        {
            return Walk(3, 1).ToString();
        }

        public override string SolveB()
        {
            var programs = new []
            {
                new[] {1, 1},
                new[] {3, 1},
                new[] {5, 1},
                new[] {7, 1},
                new[] {1, 2}
            };

            var total = programs.Aggregate(1, (sum, program) => sum * Walk(program[0], program[1]));

            return total.ToString();
        }

        private int Walk(int right, int down)
        {
            var x = right;
            var y = down;
            var width = lines[0].Length;
            var trees = 0;
            while (y < lines.Length)
            {
                if (lines[y][x] == '#')
                    trees++;
                x = (x + right) % width;
                y += down;
            }

            return trees;
        }
    }
}