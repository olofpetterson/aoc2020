using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Solutions
{
    public class Day18: BaseDay
    {
        public override string SolveA()
        {
            return lines.Aggregate("0", (sum, line) => StringAdd(Evaluate(EvaluateParentheses(line, false), false), sum));
        }

        public override string SolveB()
        {
            return lines.Aggregate("0", (sum, line) => StringAdd(Evaluate(EvaluateParentheses(line, true), true), sum));
        }

        private static string EvaluateParentheses(string input, bool evaluateAdditions)
        {                           
            var regex = new Regex(@"\([^\(\)]+\)");
            input = evaluateAdditions ? EvaluateAdditions(input) : input;
            while(regex.IsMatch(input))
            {
                var match = regex.Match(input);
                var result = Evaluate(match.Value[1..^1], evaluateAdditions);
                input = input.Replace(match.Value, result);
            }

            return input;
        }

        private static string EvaluateAdditions(string input)
        {
            var regex = new Regex(@"(\d+) \+ (\d+)");
            while(regex.IsMatch(input))
            {
                var match = regex.Match(input);
                input = input.Replace(match.Value, StringAdd(match.Groups[1].Value, match.Groups[2].Value));
            }

            return input;
        }

        private static string Evaluate(string input, bool evaluateAdditions)
        {
            input = evaluateAdditions ? EvaluateAdditions(input) : input;
            return input.Split().Aggregate(new List<string>(), (parts, part) =>
            {
                parts.Add(part);
                if (parts.Count == 3)
                {
                    parts = new List<string>
                    {
                        parts[1] switch
                        {
                            "+" => StringAdd(parts[0], parts[2]),
                            "*" => StringMultiply(parts[0], parts[2]),
                            _ => "0"
                        }
                    };
                }
                return parts;
            }).Single();
        }

        private static string StringAdd(string a, string b)
            => (long.Parse(a) + long.Parse(b)).ToString();

        private static string StringMultiply(string a, string b)
            => (long.Parse(a) * long.Parse(b)).ToString();
    }
}