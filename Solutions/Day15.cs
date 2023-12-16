using System.Collections.Specialized;
using System.Reflection.Emit;

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

        var boxes = Enumerable.Range(0, 256)
            .Select(a => new OrderedDictionary())
            .ToList();

        foreach (var instruction in input.Split(','))
        {
            var hash = 0;

            var label = instruction.Split(['=', '-']).First();

            foreach (var character in label)
            {
                hash += character;
                hash *= 17;
                hash %= 256;
            }

            if (instruction.Contains('='))
            {
                var focalLength = int.Parse(instruction.Split('=').Last());

                if (boxes[hash].Contains(label))
                {
                    boxes[hash][label] = focalLength;
                }
                else
                {
                    boxes[hash].Add(label, focalLength);
                }
            }
            else
            {
                if (boxes[hash].Contains(label))
                {
                    boxes[hash].Remove(label);
                }
            }
        }

        total = 0;

        for (var boxI = 0; boxI < boxes.Count; boxI++)
        {
            var box = boxes[boxI];

            for (var i = 0; i < box.Count; i++)
            {
                total += (boxI + 1) * (i + 1) * (int)box[i]!;
            }
        }

        Console.WriteLine($"Day15b: {total}");
    }
}