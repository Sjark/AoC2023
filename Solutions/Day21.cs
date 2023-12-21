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

        Move(grid, startPos, 64);

        Console.WriteLine($"Day 21a: {_lastTiles.Count}");
    }

    private void Move(char[,] grid, Coord location, int stepsLeft)
    {

        if (stepsLeft == 0)
        {
            _lastTiles.Add(location);
            return;
        }
        else if (_visitedTiles.Contains(location))
        {
            return;
        }

        _visitedTiles.Add(location);

        if (stepsLeft % 2 == 0)
        {
            _lastTiles.Add(location);
        }

        stepsLeft--;

        if (location.Y - 1 >= 0 && grid[location.Y - 1, location.X] != '#')
        {
            Move(grid, location with { Y = location.Y - 1 }, stepsLeft);
        }
        if (location.Y + 1 < grid.GetLength(0) && grid[location.Y + 1, location.X] != '#')
        {
            Move(grid, location with { Y = location.Y + 1 }, stepsLeft);
        }
        if (location.X - 1 >= 0 && grid[location.Y, location.X - 1] != '#')
        {
            Move(grid, location with { X = location.X - 1 }, stepsLeft);
        }
        if (location.X + 1 < grid.GetLength(1) && grid[location.Y, location.X + 1] != '#')
        {
            Move(grid, location with { X = location.X + 1 }, stepsLeft);
        }
    }
}
