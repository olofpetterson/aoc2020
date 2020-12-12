using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day08 : BaseDay
    {
        public override string SolveA()
        {
            var program = lines.Select(x => x.Split(" ")).Select(x => (x[0], int.Parse(x[1]))).ToArray();
            return  Run(program).Item2.ToString(); 
        }

        public override string SolveB()
        {
            var program = lines.Select(x => x.Split(" ")).Select(x => (x[0], int.Parse(x[1]))).ToArray();

            for (var i = 0; i < program.Length; i++)
            {
                if(program[i].Item1 == "jmp" || program[i].Item1 == "nop")
                {
                    program[i].Item1 = program[i].Item1 == "jmp" ? "nop" : "jmp";
                    var (success, accumulator) = Run(program);
                    if (success)
                    {
                        return accumulator.ToString();
                    }
                    program[i].Item1 = program[i].Item1 == "jmp" ? "nop" : "jmp";
                }
            }

            return "no solution";
        }

        private static (bool, int) Run((string, int)[] program)
        {
            var accumulator = 0;
            var visited = new HashSet<int>();
            for (var i = 0; i < program.Length;)
            {
                if (!visited.Add(i))
                {
                    return (false, accumulator);
                }

                switch (program[i].Item1)
                {
                    case "nop":
                        i++;
                        break;
                    case "acc":
                        accumulator += program[i].Item2;
                        i++;
                        break;
                    case "jmp":
                        i += program[i].Item2;
                        break;
                }
            }

            return (true, accumulator);
        }
    }
}