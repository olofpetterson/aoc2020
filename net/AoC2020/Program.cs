using System;
using System.Linq;
using System.Reflection;
using AoC2020.Solutions;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var solutions = (from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.BaseType == (typeof(BaseDay)) && t.GetConstructor(Type.EmptyTypes) != null
                select (BaseDay)Activator.CreateInstance(t)).OrderBy(t => t.GetType().Name).ToList();

            foreach (var solution in solutions)
            {
                Console.WriteLine($"{nameof(solution)} A: {solution.SolveA()}");
                Console.WriteLine($"{nameof(solution)} B: {solution.SolveB()}");
                Console.WriteLine();
            }
        }
    }
}
