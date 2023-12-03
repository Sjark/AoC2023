namespace AoC2023;

public class Day1 : ISolution
{
    public void Execute()
    {
        var numbers = new Dictionary<string, char> {
            { "one", '1' },
            { "two", '2' },
            { "three", '3' },
            { "four", '4' },
            { "five", '5' },
            { "six", '6' },
            { "seven", '7' },
            { "eight", '8' },
            { "nine", '9' }
        };

        var input = File.ReadAllLines("Solutions/Day1Input.txt");

        var total = 0;

        foreach (var line in input)
        {
            var firstNumber = line.First(char.IsAsciiDigit);
            var lastNumber = line.Last(char.IsAsciiDigit);

            total += int.Parse([firstNumber, lastNumber]);
        }

        Console.WriteLine($"A: Total: {total}");

        total = 0;

        foreach (var line in input)
        {
            char firstNumber = 'a';
            char lastNumber = 'a';

            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    lastNumber = line[i];

                    if (firstNumber == 'a')
                    {
                        firstNumber = lastNumber;
                    }
                }
                else
                {
                    var key = numbers.Keys.FirstOrDefault(a => line.AsSpan(i).StartsWith(a));

                    if (key != null)
                    {
                        lastNumber = numbers[key];

                        if (firstNumber == 'a')
                        {
                            firstNumber = lastNumber;
                        }
                    }
                }
            }

            total += int.Parse([firstNumber, lastNumber]);
        }

        Console.WriteLine($"B: Total: {total}");
    }
}
