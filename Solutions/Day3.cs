using Microsoft.VisualBasic;

namespace AoC2023;

public class Day3 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day3Input.txt");

        var total = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var isPartNumber = false;
            var currentNum = string.Empty;

            for (var j = 0; j < input[i].Length; j++)
            {
                if (char.IsDigit(input[i][j]))
                {
                    currentNum += input[i][j];

                    if (!isPartNumber)
                    {
                        var xStart = Math.Max(0, j - 1);
                        var xEnd = Math.Min(input[i].Length - 1, j + 1);
                        var yStart = Math.Max(0, i - 1);
                        var yEnd = Math.Min(input.Length - 1, i + 1);

                        for (var x = xStart; x <= xEnd; x++)
                        {
                            for (var y = yStart; y <= yEnd; y++)
                            {
                                if (IsSymbol(input[y][x]))
                                {
                                    isPartNumber = true;
                                    break;
                                }
                            }

                            if (isPartNumber)
                            {
                                break;
                            }
                        }
                    }

                    if (isPartNumber && (j == input.Length - 1 || !char.IsDigit(input[i][j + 1])))
                    {
                        total += int.Parse(currentNum);
                        currentNum = string.Empty;
                        isPartNumber = false;
                    }
                }
                else
                {
                    currentNum = string.Empty;
                }
            }
        }

        Console.WriteLine($"Day3a: {total}");

        total = 0;

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '*')
                {
                    var xStart = Math.Max(0, j - 1);
                    var xEnd = Math.Min(input[i].Length - 1, j + 1);
                    var yStart = Math.Max(0, i - 1);
                    var yEnd = Math.Min(input.Length - 1, i + 1);
                    var numbersFound = new List<int>();

                    for (var y = yStart; y <= yEnd; y++)
                    {
                        for (var x = xStart; x <= xEnd; x++)
                        {
                            if (char.IsDigit(input[y][x]))
                            {
                                var numXstart = x;
                                var numXend = x;

                                while (numXstart - 1 >= 0 && char.IsDigit(input[y][numXstart - 1]))
                                {
                                    numXstart--;
                                }

                                while (numXend + 1 < input[y].Length && char.IsDigit(input[y][numXend + 1]))
                                {
                                    numXend++;
                                }

                                var numbers = input[y][numXstart..(numXend + 1)];

                                numbersFound.Add(int.Parse(numbers));
                                x = numXend;
                            }
                        }
                    }

                    if (numbersFound.Count == 2)
                    {
                        total += numbersFound[0] * numbersFound[1];
                    }
                }
            }
        }

        Console.WriteLine($"Day3b: {total}");
    }

    public static bool IsSymbol(char character)
    {
        return !char.IsDigit(character) && character != '.';
    }
}