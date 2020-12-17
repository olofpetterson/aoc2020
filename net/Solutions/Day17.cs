using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day17: BaseDay
    {
        public override string SolveA()
        {
            var dict = new Dictionary<(int x, int y, int z), bool>();
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    dict[(x, y, 0)] = lines[y][x] == '#';
                }
            }

            var range = Enumerable.Range(-1, 3).ToArray();
            var moves = (from z in range from y in range from x in range where x != 0 || y != 0 || z != 0 select (x, y, z)).ToList();

            for (var i = 0; i < 6; i++)
            {
                var newDict = new Dictionary<(int, int, int), bool>();
                for (var z = dict.Keys.Min(x => x.z) -1; z < dict.Keys.Max(x => x.z) + 2; z++)
                {
                    for (var y = dict.Keys.Min(x => x.y) - 1; y < dict.Keys.Max(x => x.y) + 2; y++)
                    {
                        for (var x = dict.Keys.Min(a => a.x) - 1; x < dict.Keys.Max(a => a.x) + 2; x++)
                        {
                            var count = 0;
                            foreach (var move in moves)
                            {
                                if (dict.TryGetValue((x + move.x, y + move.y, z + move.z), out var active))
                                {
                                    count += active ? 1 : 0;
                                }
                            }

                            dict.TryGetValue((x, y, z), out var isActive);
                            if (isActive)
                            {
                                if (count == 2 || count == 3)
                                {
                                    newDict[(x, y, z)] = true;
                                }
                                else
                                {
                                    newDict[(x, y, z)] = false;
                                }
                            }
                            else
                            {
                                if (count == 3)
                                {
                                    newDict[(x, y, z)] = true;
                                }
                                else
                                {
                                    newDict[(x, y, z)] = false;
                                }
                            }
                        }
                    }
                }

                dict = newDict;
            }

            return dict.Count(x => x.Value).ToString();
        }

        public override string SolveB()
        {
            var dict = new Dictionary<(int x, int y, int z, int w), bool>();
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    dict[(x, y, 0, 0)] = lines[y][x] == '#';
                }
            }

            var range = Enumerable.Range(-1, 3).ToArray();
            var moves = (from w in range from z in range from y in range from x in range where x != 0 || y != 0 || z != 0 || w != 0 select (x, y, z, w)).ToList();

            for (var i = 0; i < 6; i++)
            {
                var newDict = new Dictionary<(int, int, int, int), bool>();
                for (var w = dict.Keys.Min(x => x.w) - 1; w < dict.Keys.Max(x => x.w) + 2; w++)
                {
                    for (var z = dict.Keys.Min(x => x.z) - 1; z < dict.Keys.Max(x => x.z) + 2; z++)
                    {
                        for (var y = dict.Keys.Min(x => x.y) - 1; y < dict.Keys.Max(x => x.y) + 2; y++)
                        {
                            for (var x = dict.Keys.Min(a => a.x) - 1; x < dict.Keys.Max(a => a.x) + 2; x++)
                            {
                                var count = 0;
                                foreach (var move in moves)
                                {
                                    if (dict.TryGetValue((x + move.x, y + move.y, z + move.z, w + move.w),
                                        out var active))
                                    {
                                        count += active ? 1 : 0;
                                    }
                                }

                                dict.TryGetValue((x, y, z, w), out var isActive);
                                if (isActive)
                                {
                                    if (count == 2 || count == 3)
                                    {
                                        newDict[(x, y, z, w)] = true;
                                    }
                                    else
                                    {
                                        newDict[(x, y, z, w)] = false;
                                    }
                                }
                                else
                                {
                                    if (count == 3)
                                    {
                                        newDict[(x, y, z, w)] = true;
                                    }
                                    else
                                    {
                                        newDict[(x, y, z, w)] = false;
                                    }
                                }
                            }
                        }
                    }
                }

                dict = newDict;
            }
        

            return dict.Count(x => x.Value).ToString();
        }
    }
}