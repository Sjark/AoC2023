namespace AoC2023;

public class Day6 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day6Input.txt");

        var times = input[0].Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        var distances = input[1].Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();


        var numberOfWaysToWin = new List<int>();

        for (var i = 0; i < times.Length; i++)
        {
            var numberOfWins = 0;
            var currentSpeed = 1;

            for (var y = 1; y < times[i]; y++)
            {
                var timeRemaining = times[i] - y;
                var distance = currentSpeed * timeRemaining;

                if (distance > distances[i])
                {
                    numberOfWins++;
                }

                currentSpeed++;
            }

            numberOfWaysToWin.Add(numberOfWins);
        }

        var result = numberOfWaysToWin[0];

        foreach (var way in numberOfWaysToWin.Skip(1))
        {
            result *= way;
        }

        Console.WriteLine($"Day6a: {result}");

        var time = long.Parse(input[0].Split(":")[1].Replace(" ", ""));
        var distanceToBeat = long.Parse(input[1].Split(":")[1].Replace(" ", ""));
        long numberOfWins2 = 0;
        long currentSpeed2 = 1;
        var newNumbersOfWaysToWin = new List<long>();

        for (long y = 1; y < time; y++)
        {
            var timeRemaining = time - y;
            var distance = currentSpeed2 * timeRemaining;

            if (distance > distanceToBeat)
            {
                numberOfWins2++;
            }

            currentSpeed2++;
        }

        newNumbersOfWaysToWin.Add(numberOfWins2);

        var newResult = newNumbersOfWaysToWin[0];

        foreach (var way in newNumbersOfWaysToWin.Skip(1))
        {
            newResult *= way;
        }

        Console.WriteLine($"Day6b: {newResult}");
    }
}
