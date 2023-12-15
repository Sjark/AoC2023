namespace AoC2023;

public class Day15 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllText("Solutions/Day15Input.txt").ReplaceLineEndings("");

        var total = 0;

        foreach (var instruction in input.Split(','))
        {
            var hash = 0;

            foreach (var character in instruction)
            {
                hash += character;
                hash *= 17;
                hash %= 256;
            }

            total += hash;
        }

        Console.WriteLine($"Day15a: {total}");
    }
}
