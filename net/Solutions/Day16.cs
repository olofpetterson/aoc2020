using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day16 : BaseDay
    {
        public override string SolveA()
        {
            var rules = new List<(string, (int, int), (int, int))>();
            var i = 0;
            for (; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    break;
                }
                var split = lines[i].Split(new[] {':', '-'}, StringSplitOptions.RemoveEmptyEntries);
                var split2 = split[2].Split(" or ");
                rules.Add((split[0], (int.Parse(split[1]), int.Parse(split2[0])), (int.Parse(split2[1]), int.Parse(split[3]))));
            }

            var rate = 0;
            for (i += 5; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    var split = lines[i].Split(',').Select(int.Parse);
                    
                    foreach (var value in split)
                    {
                        var isValid = false;
                        foreach (var (_, (firstLow, firstHigh), (secondLow, secondHigh)) in rules)
                        {
                            if (value >= firstLow && value <= firstHigh ||
                                value >= secondLow && value <= secondHigh)
                            {
                                isValid = true;
                                break;
                            }
                        }

                        if (!isValid)
                        {
                            rate += value;
                        }
                    }
                }
            }

            return rate.ToString();
        }

        public override string SolveB()
        {
            var rules = new List<(string, (int, int), (int, int))>();
            var i = 0;
            for (; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    break;
                }
                var split = lines[i].Split(new[] {':', '-'}, StringSplitOptions.RemoveEmptyEntries);
                var split2 = split[2].Split(" or ");
                rules.Add((split[0], (int.Parse(split[1]), int.Parse(split2[0])), (int.Parse(split2[1]), int.Parse(split[3]))));
            }

            var myTicket = lines[i + 2].Split(",").Select(int.Parse).ToArray();
            var dict = rules.ToDictionary(x => x.Item1, x => new List<int>());
            var validTickets = new HashSet<int>();
 
            for (i += 5; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }
                
                var split = lines[i].Split(',').Select(int.Parse).ToArray();
                var isTicketValid = true;
                foreach (var value in split)
                {
                    var isValueValid = false;
                    foreach (var (_, (firstLow, firstHigh), (secondLow, secondHigh)) in rules)
                    {
                        if (value >= firstLow && value <= firstHigh ||
                            value >= secondLow && value <= secondHigh)
                        {
                            isValueValid = true;
                        }
                    }

                    if (!isValueValid)
                    {
                        isTicketValid = false;
                        break;
                    }
                }

                if (isTicketValid)
                {
                    validTickets.Add(i);
                }
            }

            foreach (var (ruleName, (firstLow, firstHigh), (secondLow, secondHigh)) in rules)
            {
                for (var j = 0; j < rules.Count; j++)
                {
                    var isValid = true;
                    foreach (var validTicket in validTickets)
                    {
                        var split = lines[validTicket].Split(',').Select(int.Parse).ToArray();
                        var value = split[j];
                        if (!(value >= firstLow && value <= firstHigh ||
                              value >= secondLow && value <= secondHigh))
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        dict[ruleName].Add(j);
                    }
                }
            }

            var ruleIndex = new Dictionary<string, int>();
            foreach (var (key, value) in dict.OrderBy(x => x.Value.Count))
            {
                if (value.Except(ruleIndex.Values).Count() == 1)
                {
                    ruleIndex[key] = value.Except(ruleIndex.Values).Single();
                }
            }
            return ruleIndex.Where(x => x.Key.StartsWith("departure")).Aggregate(1L, (product, x) => myTicket[x.Value] * product).ToString();
        }
    }
}