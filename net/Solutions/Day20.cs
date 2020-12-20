using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day20 : BaseDay
    {
        public override string SolveA()
        {
            var tiles = new List<Tile>();

            for(var i = 0; i < lines.Length; i++)
            {
                var tile = new Tile { Id = int.Parse(lines[i].Split(' ', ':')[1])};
                for(i++; i < lines.Length; i++)
                {
                    if (lines[i] == "") break;
                    tile.Grid.Add(lines[i].ToArray());
                } 
                
                tiles.Add(tile);
            }

            var product = 1L;
            foreach (var tile in tiles)
            {
                var count = tile.Borders.Count(x => tiles.SelectMany(y => y.Borders).Count(z => z == x) == 2);
                if (count == 4)
                {
                    product *= tile.Id;
                }
            }

            return product.ToString();
        }

        public override string SolveB()
        {
            var tiles = new List<Tile>();

            for(var i = 0; i < lines.Length; i++)
            { 
                var tile = new Tile();
                for(i++; i < lines.Length; i++)
                {
                    if (lines[i] == "") break;
                    tile.Grid.Add(lines[i].ToArray());
                } 
                
                tiles.Add(tile);
            }
            var pictureDimension = (int)Math.Sqrt(tiles.Count);
            var corners = new List<Tile>();

            foreach (var tile in tiles)
            {
                var count = tile.Borders.Count(x => tiles.SelectMany(y => y.Borders).Count(z => z == x) == 2);
                if (count == 4)
                {
                    corners.Add(tile);
                }
            }


            var picture = Enumerable.Range(0, pictureDimension).Select(_ => new List<Tile>()).ToList();
            corners[0].RotateAndFlip(tile => tiles.SelectMany(y => y.Borders).Count(z => z == corners[0].Borders[4]) >= 2 ||  tiles.SelectMany(y => y.Borders).Count(z => z == corners[0].Borders[6]) >= 2);

            picture[0].Add(corners[0]);

            for (var y = 0; y < pictureDimension; y++)
            {
                for (var x = y == 0 ? 1 : 0; x < pictureDimension; x++)
                {
                    if (y == 0)
                    {
                        var next = tiles.Except(new[] {picture[y][x - 1]}).Single(tile => tile.Borders.Contains(picture[y][x - 1].Borders[5]));
                        next.RotateAndFlip(tile => picture[y][x - 1].Borders[5] != tile.Borders[4]);
                        picture[y].Add(next);
                    }
                    else
                    {
                        var next = tiles.Except(new[] {picture[y-1][x]}).Single(tile => tile.Borders.Contains(picture[y-1][x].Borders[7]));
                        next.RotateAndFlip(tile => picture[y-1][x].Borders[7] != next.Borders[6]);
                        picture[y].Add(next);
                    }
                }
            }

            var grid = new Tile {Grid = new List<char[]>()};

            foreach (var row in picture)
            {
                for (var i = 1; i < row[0].Grid.Count - 1; i++)
                {
                    grid.Grid.Add(row.SelectMany(x => x.Grid[i].Skip(1).Take(x.Grid[i].Length - 2)).ToArray());
                }
            }

            var cc = grid.CountMonster();
            grid.RotateAndFlip(tile => (cc = tile.CountMonster()) == 0);
            
            return (grid.Grid.Sum(x => x.Sum(y => y == '#' ? 1 : 0)) - cc * 15).ToString();
        }

        public class Tile
        {
            //                  # 
            //#    ##    ##    ###
            // #  #  #  #  #  #
            private readonly List<(int x, int y)> seaMonster = new List<(int, int)>
            {
                (18,0),
                (0,1),
                (5,1),
                (6,1),
                (11,1),
                (12,1),
                (17,1),
                (18,1),
                (19,1),
                (1,2),
                (4,2),
                (7,2),
                (10,2),
                (13,2),
                (16,2),
            };

            public int Id { get; set; }

            public List<char[]> Grid { get; set; } = new List<char[]>();

            public List<string> Borders
            {
                get
                {
                    var borders = new List<string>
                    {
                        new string(Grid.Select(x => x[0]).Reverse().ToArray()),
                        new string(Grid.Select(x => x[^1]).Reverse().ToArray()),
                        new string(Grid[0].Reverse().ToArray()),
                        new string(Grid[^1].Reverse().ToArray()),
                        new string(Grid.Select(x => x[0]).ToArray()),
                        new string(Grid.Select(x => x[^1]).ToArray()),
                        new string(Grid[0]),
                        new string(Grid[^1])
                    };
                    return borders;
                }
            }

            public void Rotate()
                => Grid = Grid.Select((_, i) => Grid.Select(row => row[i]).Reverse().ToArray()).ToList();
            
            public void Flip()
                => Grid = Grid.Select(row => row.Reverse().ToArray()).ToList();

            public void RotateAndFlip(Func<Tile, bool> predicate)
            {
                var i = 0;
                while (predicate(this))
                {
                    if(i == 4)
                        Flip();
                    Rotate();
                    i++;
                }
            }

            public int CountMonster()
            {
                var count = 0;
                for (var y = 0; y < Grid.Count - 2; y++)
                {
                    for (var x = 0; x < Grid[0].Length - 19; x++)
                    {
                        var found = seaMonster.All(move => Grid[y + move.y][x + move.x] == '#');

                        if (found)
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }
    }
}