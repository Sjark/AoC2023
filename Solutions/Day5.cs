namespace AoC2023;

public class Day5 : ISolution
{
    readonly List<Mapping> seedToSoilMap = [];
    readonly List<Mapping> soilToFertilizerMap = [];
    readonly List<Mapping> fertilizerToWaterMap = [];
    readonly List<Mapping> waterToLightMap = [];
    readonly List<Mapping> lightToTemperatureMap = [];
    readonly List<Mapping> temperatureToHumidityMap = [];
    readonly List<Mapping> humidityToLocationMap = [];

    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day5Input.txt");

        InitializeMaps(input);

        var seeds = input[0].Split(": ")[1].Split().Select(long.Parse).ToArray();
        var lowestLocation = long.MaxValue;

        foreach (var seed in seeds)
        {
            lowestLocation = Math.Min(lowestLocation, CalculatePos(seed));
        }

        Console.WriteLine($"Day5a: {lowestLocation}");

        lowestLocation = long.MaxValue;
        var lockObj = new Object();

        for (var i = 0; i < seeds.Length - 1; i += 2)
        {
            Console.WriteLine($"Seed {i} of {seeds.Length}");

            Parallel.For(seeds[i], seeds[i] + seeds[i + 1], new ParallelOptions { MaxDegreeOfParallelism = 20 }, y =>
            {
                var newLoc = CalculatePos(y);

                lock (lockObj)
                {
                    lowestLocation = Math.Min(lowestLocation, newLoc);
                }
            });
        }

        Console.WriteLine($"Day5b: {lowestLocation}");
    }

    private long CalculatePos(long seed)
    {
        var newSeed = seed;
        newSeed = GetNewPos(seedToSoilMap, newSeed);
        newSeed = GetNewPos(soilToFertilizerMap, newSeed);
        newSeed = GetNewPos(fertilizerToWaterMap, newSeed);
        newSeed = GetNewPos(waterToLightMap, newSeed);
        newSeed = GetNewPos(lightToTemperatureMap, newSeed);
        newSeed = GetNewPos(temperatureToHumidityMap, newSeed);
        newSeed = GetNewPos(humidityToLocationMap, newSeed);

        return newSeed;
    }

    private void InitializeMaps(string[] input)
    {
        var currentMap = string.Empty;

        for (var i = 1; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                continue;
            }
            else if (char.IsLetter(input[i][0]))
            {
                currentMap = input[i].Split()[0];
                continue;
            }

            var coords = input[i].Split().Select(long.Parse).ToArray();
            var map = new Mapping(coords[0], coords[1], coords[2]);

            switch (currentMap)
            {
                case "seed-to-soil":
                    seedToSoilMap.Add(map);
                    break;
                case "soil-to-fertilizer":
                    soilToFertilizerMap.Add(map);
                    break;
                case "fertilizer-to-water":
                    fertilizerToWaterMap.Add(map);
                    break;
                case "water-to-light":
                    waterToLightMap.Add(map);
                    break;
                case "light-to-temperature":
                    lightToTemperatureMap.Add(map);
                    break;
                case "temperature-to-humidity":
                    temperatureToHumidityMap.Add(map);
                    break;
                case "humidity-to-location":
                    humidityToLocationMap.Add(map);
                    break;
            }
        }
    }

    private long GetNewPos(List<Mapping> mappings, long startPos)
    {
        foreach (var map in mappings)
        {
            if (startPos >= map.SourceStart && startPos < (map.SourceStart + map.Length))
            {
                startPos += map.DestinationStart - map.SourceStart;
                break;
            }
        }

        return startPos;
    }
}

public record Mapping(long DestinationStart, long SourceStart, long Length);
