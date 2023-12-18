using System.Text.RegularExpressions;

namespace AoC2023;

public static class Helpers
{
    public static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
    {
        int max = source
            .SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => (int?)m.Value.Length))
            .Max() ?? 0;

        return source.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
    }

    public static bool IsPointInPolygon(Coord p, Coord[] polygon)
    {
        // https://wrf.ecse.rpi.edu/Research/Short_Notes/pnpoly.html
        bool inside = false;
        for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
        {
            if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                 p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
            {
                inside = !inside;
            }
        }

        return inside;
    }

    public static bool IsPointInPolygon(LongCoord p, LongCoord[] polygon)
    {
        // https://wrf.ecse.rpi.edu/Research/Short_Notes/pnpoly.html
        bool inside = false;
        for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
        {
            if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                 p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
            {
                inside = !inside;
            }
        }

        return inside;
    }
}

public record Coord(int X, int Y);
public record LongCoord(long X, long Y);