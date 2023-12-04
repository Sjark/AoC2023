using Microsoft.VisualBasic;

namespace AoC2023;

public class Day4 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day4Input.txt");

        var total = 0;

        foreach (var line in input)
        {
            var numbers = line.Split(':')[1].Trim().Split('|');
            var winningNumbers = numbers[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(a => int.Parse(a.Trim())).ToArray();
            var yourNumbers = numbers[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(a => int.Parse(a.Trim())).ToHashSet();

            var winnings = 0;

            foreach (var num in winningNumbers)
            {
                if (yourNumbers.Contains(num))
                {
                    if (winnings == 0)
                    {
                        winnings = 1;
                    }
                    else
                    {
                        winnings *= 2;
                    }
                }
            }

            total += winnings;
        }

        Console.WriteLine($"Day4a: {total}");

        var results = new Dictionary<int, long>();
        var cardNum = 1;

        foreach (var line in input)
        {
            if (results.ContainsKey(cardNum))
            {
                results[cardNum]++;
            }
            else
            {
                results[cardNum] = 1;
            }

            var numbers = line.Split(':')[1].Trim().Split('|');
            var winningNumbers = numbers[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(a => int.Parse(a.Trim())).ToArray();
            var yourNumbers = numbers[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(a => int.Parse(a.Trim())).ToHashSet();

            var nextPrice = cardNum + 1;

            foreach (var num in winningNumbers)
            {
                if (yourNumbers.Contains(num))
                {
                    if (results.ContainsKey(nextPrice))
                    {
                        results[nextPrice] += results[cardNum];
                    }
                    else
                    {
                        results[nextPrice] = results[cardNum];
                    }

                    nextPrice++;
                }
            }

            cardNum++;
        }

        var bTotal = results.Values.Sum();

        Console.WriteLine($"Day4b: {bTotal}");
    }
}