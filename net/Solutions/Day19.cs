using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Solutions
{
    public class Day19 : BaseDay
    {
        public override string SolveA()
        {
            var rules = GetRules();
            var regex = new Regex($"^{GetRegex(rules, rules["0"], 0)}$");

            return GetMessages().Count(message => regex.IsMatch(message)).ToString();
        }

        public override string SolveB()
        {
            var rules = GetRules();
            rules["8"] = "42 | 42 8".Split();
            rules["11"] = "42 31 | 42 11 31".Split();
            var regex = new Regex($"^{GetRegex(rules, rules["0"], 0)}$");

            return GetMessages().Count(message => regex.IsMatch(message)).ToString();
        }

        private Dictionary<string, string[]> GetRules()
        {
            var rules = new Dictionary<string, string[]>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) break;
                var split = line.Split(": ");
                rules[split[0]] = split[1].Split();
            }

            return rules;
        }

        private IEnumerable<string> GetMessages()
            => lines[(lines.ToList().IndexOf("") + 1)..];

        private static string GetRegex(Dictionary<string, string[]> rules, string[] rule, int recursiveDepth)
        {
            if (recursiveDepth > 15) return "";
            return rule.Aggregate(GetStartingParentheses(rule), (current, r) => current + GetInnerRegex(rules, r, recursiveDepth)) + GetEndingParentheses(rule);
        }

        private static string GetInnerRegex(Dictionary<string, string[]> rules, string token, int recursiveDepth)
        {
            return token[0] switch
            {
                '"' => token[1..2],
                '|' => "|",
                _ => GetRegex(rules, rules[token], recursiveDepth + 1)
            };
        }

        private static string GetStartingParentheses(IEnumerable<string> rule)
            => rule.Contains("|") ? "(" : "";

        private static string GetEndingParentheses(IEnumerable<string> rule)
            => rule.Contains("|") ? ")" : "";
    }
}