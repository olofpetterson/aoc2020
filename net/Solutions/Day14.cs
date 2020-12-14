using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day14 : BaseDay
    {
        public override string SolveA()
        {
            var mask = "";
            var memory = new Dictionary<long, long>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(" = ")[1];
                }
                else
                {
                    var lineSplit = line.Split(new[] {' ', '=', '[', ']'}, StringSplitOptions.RemoveEmptyEntries);
                    var (memoryAddress, value) = (long.Parse(lineSplit[1]), long.Parse(lineSplit[2]));
                    var valueArray = Convert.ToString(value, 2).PadLeft(36, '0').ToArray();

                    for (var i = 0; i < mask.Length; i++)
                    {
                        if (mask[i] != 'X')
                        {
                            valueArray[i] = mask[i];
                        }

                        memory[memoryAddress] = Convert.ToInt64(new string(valueArray), 2);
                    }
                }
            }

            return memory.Values.Sum().ToString();
        }

        public override string SolveB()
        {
            var mask = "";
            var memory = new Dictionary<long, long>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(" = ")[1];
                }
                else
                {
                    var lineSplit = line.Split(new[] {' ', '=', '[', ']'}, StringSplitOptions.RemoveEmptyEntries);
                    var (memoryAddress, value) = (long.Parse(lineSplit[1]), long.Parse(lineSplit[2]));

                    foreach (var ss in GetMemoryAddresses(mask, memoryAddress))
                    {
                        memory[ss] = value;
                    }
                }
            }

            return memory.Values.Sum().ToString();
        }

        private IEnumerable<long> GetMemoryAddresses(string mask, long value)
        {
            var unmaskedValues = new Queue<string>();
            var baseValue = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();
            
            for (var i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '0')
                {
                    continue;
                }

                baseValue[i] = mask[i];
            }

            unmaskedValues.Enqueue(new string(baseValue));

            while (unmaskedValues.Any())
            {
                var currentValue = unmaskedValues.Dequeue();
                if (currentValue.Contains('X'))
                {
                    var valueArray = currentValue.ToArray();
                    for (int i = 0; i < currentValue.Length; i++)
                    {
                        if (valueArray[i] == 'X')
                        {
                            valueArray[i] = '1';
                            unmaskedValues.Enqueue(new string(valueArray));
                            valueArray[i] = '0';
                            unmaskedValues.Enqueue(new string(valueArray));
                        }
                    }
                }
                else
                {
                    yield return Convert.ToInt64(currentValue, 2);
                }
            }
        }
    }
}