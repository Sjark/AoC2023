namespace AoC2023;

public class Day8 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day8Input.txt");

        var instructions = input[0];
        var map = new Dictionary<string, (string Left, string Right)>();

        foreach (var mapLoc in input[2..])
        {
            var key = mapLoc.Split(" = ")[0];
            var rest = mapLoc.Split(" = ")[1].Split(", ");
            map.Add(key, (rest[0].Substring(1), rest[1].Substring(0, rest[1].Length - 1)));
        }

        var steps = 0;
        var currentPos = "AAA";

        while (currentPos != "ZZZ")
        {
            var instruction = instructions[steps % instructions.Length];

            if (instruction == 'L')
            {
                currentPos = map[currentPos].Left;
            }
            else
            {
                currentPos = map[currentPos].Right;
            }

            steps++;
        }

        Console.WriteLine($"Day8a: {steps}");

        var currentPositions = map.Keys
            .Where(a => a.EndsWith('A'))
            .Select(a => CalulateStepsToEnd(a, instructions, map))
            .ToList();

        var steps2 = (long)currentPositions[0];
        for (var i = 1; i < currentPositions.Count; i++)
        {
            steps2 = CalculateLeastCommonMultiple(steps2, currentPositions[i]);
        }

        Console.WriteLine($"Day8b: {steps2}");
    }

    private int CalulateStepsToEnd(string startNode, string instructions, Dictionary<string, (string Left, string Right)> map)
    {
        var steps = 0;

        while (!startNode.EndsWith('Z'))
        {
            var instruction = instructions[steps % instructions.Length];
            startNode = instruction == 'L' ? map[startNode].Left : map[startNode].Right;
            steps++;
        }

        return steps;
    }

    private long CalculateGreatestCommonFactor(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    private long CalculateLeastCommonMultiple(long a, long b)
    {
        return a / CalculateGreatestCommonFactor(a, b) * b;
    }
}
