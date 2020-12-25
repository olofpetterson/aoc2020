using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day24 : BaseDay
    {
        private readonly List<(int x, int y)> moves = new List<(int, int)>
        {
            (0, 1),
            (1, 1),
            (-1, 0),
            (1, 0),
            (-1, -1),
            (0, -1)
        };

        public override string SolveA()
        {
            return FlipTiles().Count.ToString();
        }

        public override string SolveB()
        {

            var blackTiles = FlipTiles();

            for (var i = 0; i < 100; i++)
            {
                var newHash = new HashSet<(int, int)>();
                var xMin = blackTiles.Min(x => x.Item1) - 2;
                var xMax = blackTiles.Max(x => x.Item1) + 2;
                var yMin = blackTiles.Min(x => x.Item2) - 2;
                var yMax = blackTiles.Max(x => x.Item2) + 2;

                for (var y = yMin; y <= yMax; y++)
                {
                    for (var x = xMin; x < xMax; x++)
                    {
                        var count = moves.Count(move => blackTiles.Contains((x + move.x, y + move.y)));

                        if (!blackTiles.Contains((x, y)) && count == 2)
                        {
                            newHash.Add((x, y));
                        }
                        else if (blackTiles.Contains((x, y)) && (count != 0 && count <= 2))
                        {
                            newHash.Add((x, y));
                        }

                    }
                }

                blackTiles = newHash;
            }
            
            return blackTiles.Count.ToString();
        }

        private HashSet<(int, int)> FlipTiles()
        {
            var blackTiles = new HashSet<(int, int)>();

            foreach (var line in lines)
            {
                var instructions = line;
                var pos = (0, 0);
                while (instructions != "")
                {
                    switch (instructions)
                    {
                        case { } a when a.StartsWith("e"):
                            pos = (pos.Item1 + 1, pos.Item2);
                            instructions = instructions[1..];
                            break;
                        case { } a when a.StartsWith("w"):
                            pos = (pos.Item1 - 1, pos.Item2);
                            instructions = instructions[1..];
                            break;
                        case { } a when a.StartsWith("ne"):
                            pos = (pos.Item1 + 1, pos.Item2 + 1);
                            instructions = instructions[2..];
                            break;
                        case { } a when a.StartsWith("nw"):
                            pos = (pos.Item1, pos.Item2 + 1);
                            instructions = instructions[2..];
                            break;
                        case { } a when a.StartsWith("se"):
                            pos = (pos.Item1, pos.Item2 - 1);
                            instructions = instructions[2..];
                            break;
                        case { } a when a.StartsWith("sw"):
                            pos = (pos.Item1 - 1, pos.Item2 - 1);
                            instructions = instructions[2..];
                            break;

                    }
                }

                if (blackTiles.Contains(pos))
                {
                    blackTiles.Remove(pos);
                }
                else
                {
                    blackTiles.Add(pos);
                }
            }

            return blackTiles;
        }
    }
}