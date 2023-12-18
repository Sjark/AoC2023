namespace AoC2023;

public class Day10 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day10Input.txt");

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

        var directions = new List<Coord>();
        char[] northPositions = ['|', '7', 'F'];
        char[] southPositions = ['|', 'L', 'J'];
        char[] westPositions = ['-', 'L', 'F'];
        char[] eastPositions = ['-', 'J', '7'];

        if (northPositions.Contains(grid[startPos.Y - 1, startPos.X]))
        {
            directions.Add(new Coord(startPos.X, startPos.Y - 1));
        }
        if (eastPositions.Contains(grid[startPos.Y, startPos.X + 1]))
        {
            directions.Add(new Coord(startPos.X + 1, startPos.Y));
        }
        if (southPositions.Contains(grid[startPos.Y + 1, startPos.X]))
        {
            directions.Add(new Coord(startPos.X, startPos.Y + 1));
        }
        if (westPositions.Contains(grid[startPos.Y, startPos.X - 1]))
        {
            directions.Add(new Coord(startPos.X - 1, startPos.Y));
        }

        var prevPos1 = startPos;
        var prevPos2 = startPos;
        var curPos1 = directions.First();
        var curPos2 = directions.Last();

        var steps = 1;

        while (curPos1 != curPos2)
        {
            steps++;
            var tempCurPos = curPos1;
            curPos1 = Move(grid, prevPos1, curPos1);
            prevPos1 = tempCurPos;
            tempCurPos = curPos2;
            curPos2 = Move(grid, prevPos2, curPos2);
            prevPos2 = tempCurPos;
        }

        var polygon = new List<Coord> {
            startPos
        };

        curPos1 = directions.First();
        prevPos1 = startPos;

        while (curPos1 != startPos)
        {
            polygon.Add(curPos1);
            var tempCur = curPos1;
            curPos1 = Move(grid, prevPos1, curPos1);
            prevPos1 = tempCur;
        }

        Console.WriteLine($"Day10a: {steps}");

        var area = 0;

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (!polygon.Contains(new Coord(x, y)) && Helpers.IsPointInPolygon(new Coord(x, y), [.. polygon]))
                {
                    area++;
                }
            }
        }

        Console.WriteLine($"Day10b: {area}");
    }



    private Coord Move(char[,] grid, Coord previousPos, Coord currentPos)
    {
        return grid[currentPos.Y, currentPos.X] switch
        {
            '|' => previousPos == new Coord(currentPos.X, currentPos.Y + 1) ? new Coord(currentPos.X, currentPos.Y - 1) : new Coord(currentPos.X, currentPos.Y + 1),
            '-' => previousPos == new Coord(currentPos.X + 1, currentPos.Y) ? new Coord(currentPos.X - 1, currentPos.Y) : new Coord(currentPos.X + 1, currentPos.Y),
            'L' => previousPos == new Coord(currentPos.X + 1, currentPos.Y) ? new Coord(currentPos.X, currentPos.Y - 1) : new Coord(currentPos.X + 1, currentPos.Y),
            'J' => previousPos == new Coord(currentPos.X - 1, currentPos.Y) ? new Coord(currentPos.X, currentPos.Y - 1) : new Coord(currentPos.X - 1, currentPos.Y),
            '7' => previousPos == new Coord(currentPos.X - 1, currentPos.Y) ? new Coord(currentPos.X, currentPos.Y + 1) : new Coord(currentPos.X - 1, currentPos.Y),
            'F' => previousPos == new Coord(currentPos.X, currentPos.Y + 1) ? new Coord(currentPos.X + 1, currentPos.Y) : new Coord(currentPos.X, currentPos.Y + 1),
            _ => throw new Exception("HOW IS THIS POSSIBLE"),
        };
    }
}
