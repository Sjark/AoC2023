using Microsoft.VisualBasic;

namespace AoC2023;

public class Day21 : ISolution
{
    private readonly HashSet<Coord> _visitedTiles = [];
    private readonly HashSet<Coord> _lastTiles = [];

    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day21Input.txt");

        var grid = new char[input.Length, input[0].Length];

        var startPos = new Coord(-1, -1);

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                grid[y, x] = input[y][x];

                if (input[y][x] == 'S')
                {
                    startPos = new Coord(x, y);
                }
            }
        }

        var queue = new Queue<(Coord Coord, int StepsLeft)>();
        queue.Enqueue((startPos, 64));

        while (queue.TryDequeue(out var currentTile))
        {
            if (currentTile.StepsLeft == 0)
            {
                _lastTiles.Add(currentTile.Coord);
                continue;
            }
            else if (_visitedTiles.Contains(currentTile.Coord) && currentTile.StepsLeft % 2 == 0)
            {
                _lastTiles.Add(currentTile.Coord);
                continue;
            }

            if (currentTile.Coord.Y - 1 >= 0 && grid[currentTile.Coord.Y - 1, currentTile.Coord.X] != '#')
            {
                queue.Enqueue((currentTile.Coord with { Y = currentTile.Coord.Y - 1 }, currentTile.StepsLeft - 1));
            }
            if (currentTile.Coord.Y + 1 < grid.GetLength(0) && grid[currentTile.Coord.Y + 1, currentTile.Coord.X] != '#')
            {
                queue.Enqueue((currentTile.Coord with { Y = currentTile.Coord.Y + 1 }, currentTile.StepsLeft - 1));
            }
            if (currentTile.Coord.X - 1 >= 0 && grid[currentTile.Coord.Y, currentTile.Coord.X - 1] != '#')
            {
                queue.Enqueue((currentTile.Coord with { X = currentTile.Coord.X - 1 }, currentTile.StepsLeft - 1));
            }
            if (currentTile.Coord.X + 1 < grid.GetLength(1) && grid[currentTile.Coord.Y, currentTile.Coord.X + 1] != '#')
            {
                queue.Enqueue((currentTile.Coord with { X = currentTile.Coord.X + 1 }, currentTile.StepsLeft - 1));
            }

            _visitedTiles.Add(currentTile.Coord);
        }

        Console.WriteLine($"Day 21a: {_lastTiles.Count}");
    }
}
