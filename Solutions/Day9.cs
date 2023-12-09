namespace AoC2023;

public class Day9 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day9Input.txt");

        var result = 0;

        foreach (var line in input)
        {
            var numbers = line.Split()
                .Select(int.Parse)
                .ToList();

            var resultingList = new List<List<int>>
            {
                numbers
            };

            while (!resultingList.Last().All(a => a == 0))
            {
                var toCalculate = resultingList.Last();
                var newList = new List<int>();

                for (int i = 0; i < toCalculate.Count - 1; i++)
                {
                    newList.Add(toCalculate[i + 1] - toCalculate[i]);
                }

                resultingList.Add(newList);
            }

            resultingList.Last().Add(0);

            for (var i = resultingList.Count; i > 1; i--)
            {
                resultingList[i - 2].Add(resultingList[i - 1].Last() + resultingList[i - 2].Last());
            }

            result += resultingList[0].Last();
        }

        Console.WriteLine($"Day9a: {result}");

        result = 0;

        foreach (var line in input)
        {
            var numbers = line.Split()
                .Select(int.Parse)
                .ToList();

            var resultingList = new List<List<int>>
            {
                numbers
            };

            while (!resultingList.Last().All(a => a == 0))
            {
                var toCalculate = resultingList.Last();
                var newList = new List<int>();

                for (int i = 0; i < toCalculate.Count - 1; i++)
                {
                    newList.Add(toCalculate[i + 1] - toCalculate[i]);
                }

                resultingList.Add(newList);
            }

            resultingList.Last().Add(0);

            for (var i = resultingList.Count; i > 1; i--)
            {
                resultingList[i - 2].Insert(0, resultingList[i - 2][0] - resultingList[i - 1][0]);
            }

            result += resultingList[0][0];
        }

        Console.WriteLine($"Day9b: {result}");
    }
}
