using AoC2023;
using System.Reflection;

var days = new Dictionary<string, ISolution>();
foreach (var day in Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(type => typeof(ISolution).IsAssignableFrom(type) && !type.IsInterface))
{
    var instance = (ISolution?)Activator.CreateInstance(day);

    if (instance != null)
    {
        days.Add(day.Name, instance);
    }
}

while (true)
{
    Console.WriteLine("Please select a day to run:");
    Console.WriteLine();

    var i = 1;

    var daysSorted = days.Keys.OrderByAlphaNumeric(a => a).ToList();

    foreach (var day in days.Keys.OrderByAlphaNumeric(a => a))
    {
        Console.WriteLine($"{i}: {day}");

        i++;
    }

    Console.WriteLine();
    Console.Write("Enter day: ");
    var selectedDay = Console.ReadLine();
    int selectedDayNum;

    while (!int.TryParse(selectedDay, out selectedDayNum) || selectedDayNum - 1 < 0 || selectedDayNum > daysSorted.Count)
    {
        Console.WriteLine();
        Console.WriteLine("Invalid day entered");
        Console.Write("Enter day: ");
        selectedDay = Console.ReadLine();
    }

    Console.WriteLine();
    Console.WriteLine($"Running {daysSorted[selectedDayNum - 1]}, result:");
    Console.WriteLine();

    days[daysSorted[selectedDayNum - 1]].Execute();

    Console.WriteLine();
    Console.WriteLine("Press <enter> to get back to main menu");
    Console.ReadLine();
    Console.Clear();
}


