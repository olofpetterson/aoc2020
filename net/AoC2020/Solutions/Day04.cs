using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Solutions
{
    public class Day04 : BaseDay
    {
        private static readonly HashSet<string> eclHash = new HashSet<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        
        private static readonly Dictionary<string, Func<string, bool>> ruleDictionary = new Dictionary<string, Func<string, bool>>
        {
            {"byr", input => int.Parse(input) switch {{ } byr when byr >= 1920 && byr <= 2002 => true, _ => false}},
            {"iyr", input => int.Parse(input) switch {{ } iyr when iyr >= 2010 && iyr <= 2020 => true, _ => false}},
            {"eyr", input => int.Parse(input) switch {{ } eyr when eyr >= 2020 && eyr <= 2030 => true, _ => false}},
            {"hgt", input => input.Length > 2 && (input[^2..], int.Parse(input[..^2])) switch 
            { { } hgt when hgt.Item1 == "cm" && hgt.Item2 >= 150 && hgt.Item2 <= 193 => true,
                { } hgt when hgt.Item1 == "in" && hgt.Item2 >= 59 && hgt.Item2 <= 76 => true,
                _ => false }},
            {"hcl", input => new Regex("^[a-zA-Z0-9]*$").IsMatch(input[1..]) && input[0] == '#'},
            {"ecl", input => eclHash.Contains(input)},
            {"pid", input => new Regex("^[0-9]*$").IsMatch(input[1..]) && input.Length == 9},
            {"cid", input => false}
        };

        public override string SolveA()
        {
            var input = data.Split(Environment.NewLine + Environment.NewLine).Select(chunk => chunk.Split(Environment.NewLine)).ToArray();
            var validPassports = 0;

            foreach (var passport in input)
            {
                var hash = new HashSet<string>();
                foreach (var line in passport)
                {
                    foreach (var field in line.Split(" ").Select(x => x.Split(":")[0]))
                    {
                        hash.Add(field);
                    }
                }
                validPassports += hash.Count == 8 || hash.Count == 7 && !hash.Contains("cid") ? 1 : 0;
            }

            return validPassports.ToString();
        }

        public override string SolveB()
        {
            var input = data.Split(Environment.NewLine + Environment.NewLine).Select(chunk => chunk.Split(Environment.NewLine)).ToArray();
            var validPassports = 0;

            foreach (var passport in input)
            {
                var validFields = 0;
                foreach (var line in passport)
                {
                    foreach (var field in line.Split(" ").Select(x => x.Split(":")))
                    {
                        validFields += ruleDictionary[field[0]].Invoke(field[1]) ? 1 : 0;
                    }
                }
                validPassports += validFields == 7 ? 1 : 0;
            }


            return validPassports.ToString();
        }
    }
}