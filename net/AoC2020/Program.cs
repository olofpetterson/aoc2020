using AoC2020.Solutions;
using System;
using System.Linq;
using System.Reflection;

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
                Console.WriteLine(solution.GetType().Name);
                Console.WriteLine($"A: {solution.SolveA()}");
                Console.WriteLine($"B: {solution.SolveB()}");
                Console.WriteLine();
            }
        }
    }
}
