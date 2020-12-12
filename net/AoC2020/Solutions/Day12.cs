using System;
using System.Linq;

namespace AoC2020.Solutions
{
    public class Day12 : BaseDay
    {
        public override string SolveA()
        {
            var instructions = lines.Select(line => (line[0], int.Parse(line[1..]))).ToArray();

            var direction = 1;
            var x = 0;
            var y = 0;
            
            foreach (var (command, value) in instructions)
            {
                switch (command)
                {
                    case 'N':
                        y += value;
                        break;
                    case 'S':
                        y -= value;
                        break;
                    case 'W':
                        x -= value;
                        break;
                    case 'E':
                        x += value;
                        break;
                    case 'L':
                        direction = (direction + (4 - value / 90)) % 4;
                        break;
                    case 'R':
                        direction = (direction + value / 90) % 4;
                        break;
                    case 'F':
                        y += direction switch
                        {
                            0 => value,
                            2 => -value,
                            _ => 0
                        };
                        x += direction switch
                        {
                            1 => value,
                            3 => -value,
                            _ => 0
                        };
                        break;
                }

            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public override string SolveB()
        {           
            var instructions = lines.Select(line => (line[0], int.Parse(line[1..]))).ToArray();

            var waypointX = 10;
            var waypointY = 1;
            var shipX = 0;
            var shipY = 0;

            foreach (var (command, value) in instructions)
            {
                switch (command)
                {
                    case 'N':
                        waypointY += value;
                        break;
                    case 'S':
                        waypointY -= value;
                        break;
                    case 'W':
                        waypointX -= value;
                        break;
                    case 'E':
                        waypointX += value;
                        break;
                    case 'L':
                        for (var i = 0; i < value / 90; i++)
                        {
                            var temp = waypointX;
                            waypointX = -waypointY;
                            waypointY = temp;
                        }
                        break;
                    case 'R':
                        for (var i = 0; i < value / 90; i++)
                        {
                            var temp = waypointX;
                            waypointX = waypointY;
                            waypointY = -temp;
                        }
                        break;
                    case 'F':
                        shipX += value * waypointX;
                        shipY += value * waypointY;
                        break;
                }

            }

            return (Math.Abs(shipX) + Math.Abs(shipY)).ToString();
        }
    }
}