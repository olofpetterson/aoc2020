using System.Linq;

namespace AoC2020.Solutions
{
    public class Day11 : BaseDay
    {
        private readonly (int, int)[] moves = 
        {
            (-1, -1),
            (0, -1),
            (1, -1),
            (-1, 0),
            (1, 0),
            (-1, 1),
            (0, 1),
            (1, 1)
        };

        public override string SolveA()
        {
            var oldGrid = lines.Select(s => s.ToArray()).ToArray();
            var newGrid = lines.Select(s => s.ToArray()).ToArray();
            var width = oldGrid[0].Length;
            var height = oldGrid.Length;
            var changes = true;

            while (changes)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (oldGrid[y][x] == '.') continue;
                        var count = 0;
                        foreach (var (dx, dy) in moves)
                        {
                            var testX = x + dx;
                            var testY = y + dy;
                            if (testX >= 0 && testX < width && testY >= 0 && testY < height)
                            {
                                count += oldGrid[testY][testX] == '#' ? 1 : 0;
                            }
                        }

                        newGrid[y][x] = oldGrid[y][x] switch
                        {
                            'L' when count == 0 => '#',
                            '#' when count > 3 => 'L',
                            _ => oldGrid[y][x]
                        };
                    }
                }

                changes = false;
                for (var y = 0; y < height; y++)
                {
                    if (oldGrid[y].SequenceEqual(newGrid[y])) continue;
                    changes = true;
                    break;
                }

                var temp = oldGrid;
                oldGrid = newGrid;
                newGrid = temp;
            }

            return oldGrid.Select(x => x.Count(y => y == '#')).Sum().ToString();
        }

        public override string SolveB()
        {            
            var oldGrid = lines.Select(s => s.ToArray()).ToArray();
            var newGrid = lines.Select(s => s.ToArray()).ToArray();
            var width = oldGrid[0].Length;
            var height = oldGrid.Length;
            var changes = true;

            while (changes)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (oldGrid[y][x] == '.') continue;
                        var count = 0;
                        foreach (var (dx, dy) in moves)
                        {
                            var testX = x + dx;
                            var testY = y + dy;
                            var seen = false;
                            while (testX >= 0 && testX < width && testY >= 0 && testY < height && !seen)
                            {
                                seen = oldGrid[testY][testX] == '#' || oldGrid[testY][testX] == 'L';
                                count += oldGrid[testY][testX] == '#' ? 1 : 0;
                                testX += dx;
                                testY += dy;
                            }
                        }

                        newGrid[y][x] = oldGrid[y][x] switch
                        {
                            'L' when count == 0 => '#',
                            '#' when count > 4 => 'L',
                            _ => oldGrid[y][x]
                        };
                    }
                }

                changes = false;
                for (var y = 0; y < height; y++)
                {
                    if (oldGrid[y].SequenceEqual(newGrid[y])) continue;
                    changes = true;
                    break;
                }

                var temp = oldGrid;
                oldGrid = newGrid;
                newGrid = temp;
            }

            return oldGrid.Select(x => x.Count(y => y == '#')).Sum().ToString();
        }
    }
}