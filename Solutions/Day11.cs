
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

        var coords = GetCoords(grid);
        var combinations = coords.SelectMany((x, i) => coords.Skip(i + 1), (x, y) => Tuple.Create(x, y)).ToList();

        var total = CalculateTotalDistance(rowsWithoutGalaxies, columnsWithoutGalaxies, combinations, 1);

        Console.WriteLine($"Day11a: {total}");

        var newTotal = CalculateTotalDistance(rowsWithoutGalaxies, columnsWithoutGalaxies, combinations, 999999);
        Console.WriteLine($"Day11b: {newTotal}");
    }

    private static long CalculateTotalDistance(List<int> rowsWithoutGalaxies, List<int> columnsWithoutGalaxies, List<Tuple<Coord, Coord>> combinations, long extraSpace)
    {
        var total = 0L;

        foreach (var comb in combinations)
        {
            var columns = Math.Abs(comb.Item1.X - comb.Item2.X);
            var rows = Math.Abs(comb.Item1.Y - comb.Item2.Y);
            var extraColumnSpaces = columnsWithoutGalaxies
                .Where(a => a > Math.Min(comb.Item1.Y, comb.Item2.Y) && a < Math.Max(comb.Item1.Y, comb.Item2.Y))
                .Count();
            var extraRowSpaces = rowsWithoutGalaxies
                .Where(a => a > Math.Min(comb.Item1.X, comb.Item2.X) && a < Math.Max(comb.Item1.X, comb.Item2.X))
                .Count();

            total += columns + rows + (extraSpace * extraColumnSpaces) + (extraSpace * extraRowSpaces);
        }

        return total;
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
