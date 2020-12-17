using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day17: BaseDay
    {
        public override string SolveA()
        {
            var activeCubes = new HashSet<(int x, int y, int z)>();
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '#')
                        activeCubes.Add((x, y, 0));
                }
            }

            var range = Enumerable.Range(-1, 3).ToArray();
            var moves = (from z in range from y in range from x in range where x != 0 || y != 0 || z != 0 select (x, y, z)).ToArray();

            for (var i = 0; i < 6; i++)
            {
                var newActiveCubes = new HashSet<(int x, int y, int z)>();
                for (var z = activeCubes.Min(x => x.z) -1; z < activeCubes.Max(x => x.z) + 2; z++)
                {
                    for (var y = activeCubes.Min(x => x.y) - 1; y < activeCubes.Max(x => x.y) + 2; y++)
                    {
                        for (var x = activeCubes.Min(a => a.x) - 1; x < activeCubes.Max(a => a.x) + 2; x++)
                        {
                            var count = moves.Count(move => activeCubes.Contains((x + move.x, y + move.y, z + move.z)));

                            if (activeCubes.Contains((x, y, z)) && (count == 2 || count == 3))
                            {
                                newActiveCubes.Add((x, y, z));
                            }
                            else if (count == 3)
                            {
                                newActiveCubes.Add((x, y, z));
                            }
                        }
                    }
                }

                activeCubes = newActiveCubes;
            }

            return activeCubes.Count.ToString();
        }

        public override string SolveB()
        {
            var activeCubes = new HashSet<(int x, int y, int z, int w)>();
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    if (lines[y][x] == '#')
                        activeCubes.Add((x, y, 0, 0));
                }
            }

            var range = Enumerable.Range(-1, 3).ToArray();
            var moves = (from w in range from z in range from y in range from x in range where x != 0 || y != 0 || z != 0 || w != 0 select (x, y, z, w)).ToList();

            for (var i = 0; i < 6; i++)
            {
                var newActiveCubes = new HashSet<(int, int, int, int)>();
                for (var w = activeCubes.Min(x => x.w) - 1; w < activeCubes.Max(x => x.w) + 2; w++)
                {
                    for (var z = activeCubes.Min(x => x.z) - 1; z < activeCubes.Max(x => x.z) + 2; z++)
                    {
                        for (var y = activeCubes.Min(x => x.y) - 1; y < activeCubes.Max(x => x.y) + 2; y++)
                        {
                            for (var x = activeCubes.Min(a => a.x) - 1; x < activeCubes.Max(a => a.x) + 2; x++)
                            {
                                var count = moves.Count(move => activeCubes.Contains((x + move.x, y + move.y, z + move.z, w + move.w)));

                                if (activeCubes.Contains((x, y, z, w)) && (count == 2 || count == 3))
                                {
                                    newActiveCubes.Add((x, y, z, w));
                                }
                                else if (count == 3)
                                {
                                    newActiveCubes.Add((x, y, z, w));
                                }
                            }
                        }
                    }
                }

                activeCubes = newActiveCubes;
            }

            return activeCubes.Count.ToString();
        }
    }
}