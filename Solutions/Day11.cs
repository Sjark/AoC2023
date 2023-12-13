
namespace AoC2023;

public class Day11 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day11Input.txt");

        var grid = new List<List<char>>();
        var rowsWithoutGalaxies = new List<int>();
        var columnsWithoutGalaxies = new List<int>();

        for (var y = 0; y < input.Length; y++)
        {
            var containsGalaxy = false;
            grid.Add([]);
            for (var x = 0; x < input[y].Length; x++)
            {
                grid[y].Add(input[y][x]);

                if (input[y][x] == '#')
                {
                    containsGalaxy = true;
                }
            }

            if (!containsGalaxy)
            {
                rowsWithoutGalaxies.Add(y);
            }
        }

        for (var x = 0; x < input[0].Length; x++)
        {
            var containsGalaxy = false;
            for (var y = 0; y < input.Length; y++)
            {
                if (input[y][x] == '#')
                {
                    containsGalaxy = true;
                }
            }

            if (!containsGalaxy)
            {
                columnsWithoutGalaxies.Add(x);
            }
        }

        var offset = 0;

        foreach (var row in rowsWithoutGalaxies)
        {
            grid.Insert(row + 1 + offset, [.. grid[row + offset]]);
            offset++;
        }

        offset = 0;
        foreach (var column in columnsWithoutGalaxies)
        {
            for (var x = 0; x < grid.Count; x++)
            {
                grid[x].Insert(column + offset, '.');
            }

            offset++;
        }

        var coords = GetCoords(grid);
        var combinations = coords.SelectMany((x, i) => coords.Skip(i + 1), (x, y) => Tuple.Create(x, y)).ToList();

        var total = 0;

        foreach (var comb in combinations)
        {
            total += Math.Abs(comb.Item1.X - comb.Item2.X) + Math.Abs(comb.Item1.Y - comb.Item2.Y);
        }

        Console.WriteLine($"Day11a: {total}");
    }

    private List<Coord> GetCoords(List<List<char>> grid)
    {
        List<Coord> coords = [];

        for (int x = 0; x < grid.Count; x++)
        {
            for (int y = 0; y < grid[x].Count; y++)
            {
                if (grid[x][y] == '#')
                {
                    coords.Add(new Coord(x, y));
                }
            }
        }

        return coords;
    }
}
