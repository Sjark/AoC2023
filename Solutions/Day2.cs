namespace AoC2023;

public class Day2 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day2Input.txt");

        var maxNumRed = 12;
        var maxNumGreen = 13;
        var maxNumBlue = 14;

        var total = 0;

        foreach (var line in input)
        {
            var splittedLine = line.Split(':');
            var gameNumber = int.Parse(splittedLine[0].Split(' ')[1]);

            var gameSets = splittedLine[1].Split(';');
            var failed = false;

            foreach (var set in gameSets)
            {
                var colors = set.Split(',').Select(a => a.Trim());
                var numRed = 0;
                var numBlue = 0;
                var numGreen = 0;

                foreach (var color in colors)
                {
                    var colorSplitted = color.Split(' ');

                    var number = int.Parse(colorSplitted[0]);

                    if (colorSplitted[1] == "red")
                    {
                        numRed += number;
                    }
                    else if (colorSplitted[1] == "green")
                    {
                        numGreen += number;
                    }
                    else if (colorSplitted[1] == "blue")
                    {
                        numBlue += number;
                    }
                }

                if (numRed > maxNumRed || numBlue > maxNumBlue || numGreen > maxNumGreen)
                {
                    failed = true;
                    break;
                }
            }

            if (failed)
            {
                continue;
            }

            total += gameNumber;
        }

        Console.WriteLine($"Total is {total}");

        total = 0;

        foreach (var line in input)
        {
            var splittedLine = line.Split(':');
            var gameNumber = int.Parse(splittedLine[0].Split(' ')[1]);

            var gameSets = splittedLine[1].Split(';');

            var maxRed = 0;
            var maxBlue = 0;
            var maxGreen = 0;

            foreach (var set in gameSets)
            {
                var colors = set.Split(',').Select(a => a.Trim());
                var numRed = 0;
                var numBlue = 0;
                var numGreen = 0;

                foreach (var color in colors)
                {
                    var colorSplitted = color.Split(' ');

                    var number = int.Parse(colorSplitted[0]);

                    if (colorSplitted[1] == "red")
                    {
                        numRed += number;
                    }
                    else if (colorSplitted[1] == "green")
                    {
                        numGreen += number;
                    }
                    else if (colorSplitted[1] == "blue")
                    {
                        numBlue += number;
                    }
                }

                if (numRed > maxRed)
                {
                    maxRed = numRed;
                }
                if (numBlue > maxBlue)
                {
                    maxBlue = numBlue;
                }
                if (numGreen > maxGreen)
                {
                    maxGreen = numGreen;
                }
            }

            System.Console.WriteLine($"Game {gameNumber}: {maxRed} red, {maxBlue} blue, {maxGreen} green");

            total += maxRed * maxBlue * maxGreen;
        }


        Console.WriteLine($"Day2B: Total is {total}");
    }
}