namespace AoC2023;

public class Day7 : ISolution
{
    public void Execute()
    {
        var input = File.ReadAllLines("Solutions/Day7Input.txt");
        var hands = input.Select(a =>
        {
            var splitted = a.Split(' ').ToArray();
            var cards = new List<int>();

            foreach (var card in splitted[0])
            {
                cards.Add(CardToNum(card));
            }

            return new Hand
            {
                Bid = int.Parse(splitted[1]),
                Cards = cards,
                Type = GetHandType(cards)
            };
        }).ToList();

        var cardsComparer = new CardsComparer();
        var handsSorted = hands
            .OrderByDescending(a => a.Type)
            .ThenBy(a => a.Cards, cardsComparer)
            .ToList();
        var rank = handsSorted.Count;

        var results = 0L;

        foreach (var hand in handsSorted)
        {
            results += hand.Bid * rank;
            rank--;
        }

        Console.WriteLine($"Day7a: {results}");

        var jokerHands = input.Select(a =>
        {
            var splitted = a.Split(' ').ToArray();
            var cards = new List<int>();

            foreach (var card in splitted[0])
            {
                cards.Add(CardToNum(card, true));
            }

            return new Hand
            {
                Bid = int.Parse(splitted[1]),
                Cards = cards,
                Type = GetHandType(cards, true)
            };
        }).ToList();

        var jokerHandsSorted = jokerHands
            .OrderByDescending(a => a.Type)
            .ThenBy(a => a.Cards, cardsComparer)
            .ToList();
        var jokerRank = jokerHandsSorted.Count;

        var jokerResults = 0L;

        foreach (var hand in jokerHandsSorted)
        {
            jokerResults += hand.Bid * jokerRank;
            jokerRank--;
        }

        Console.WriteLine($"Day7b: {jokerResults}");
    }

    private int CardToNum(char card, bool joker = false)
    {
        if (char.IsAsciiDigit(card))
        {
            return card - '0';
        }

        if (card == 'T')
        {
            return 10;
        }
        if (card == 'J')
        {
            return joker ? -1 : 11;
        }
        if (card == 'Q')
        {
            return 12;
        }
        if (card == 'K')
        {
            return 13;
        }
        if (card == 'A')
        {
            return 14;
        }

        throw new Exception("Invalid card");
    }

    private HandType GetHandType(List<int> cards, bool joker = false)
    {
        var dict = cards.Aggregate(new Dictionary<int, int>(), (dict, currentValue) =>
        {
            if (dict.ContainsKey(currentValue))
            {
                dict[currentValue] += 1;
            }
            else
            {
                dict[currentValue] = 1;
            }

            return dict;
        });

        if (joker && dict.ContainsKey(-1))
        {
            var highestValue = dict.Where(a => a.Key != -1).OrderByDescending(a => a.Value);

            if (highestValue.Count() > 0)
            {
                dict[highestValue.First().Key] += dict[-1];
                dict.Remove(-1);
            }
        }

        if (dict.Values.Any(a => a == 5))
        {
            return HandType.FiveOfKind;
        }
        if (dict.Values.Any(a => a == 4))
        {
            return HandType.FourOfKind;
        }
        if (dict.Count == 2)
        {
            return HandType.FullHouse;
        }
        if (dict.Values.Any(a => a == 3))
        {
            return HandType.ThreeOfKind;
        }
        if (dict.Values.Where(a => a == 2).Count() == 2)
        {
            return HandType.TwoPair;
        }
        if (dict.Values.Any(a => a == 2))
        {
            return HandType.OnePair;
        }

        return HandType.HighCard;
    }
}

public class Hand
{
    public List<int> Cards { get; set; } = [];
    public int Bid { get; set; }
    public HandType Type { get; set; }
}

public enum HandType
{
    FiveOfKind = 6,
    FourOfKind = 5,
    FullHouse = 4,
    ThreeOfKind = 3,
    TwoPair = 2,
    OnePair = 1,
    HighCard = 0
}

public class CardsComparer : IComparer<List<int>>
{
    public int Compare(List<int>? x, List<int>? y)
    {
        if (x == null && y == null)
        {
            return 0;
        }
        else if (x == null)
        {
            return -1;
        }
        else if (y == null)
        {
            return 1;
        }

        for (var i = 0; i < x.Count; i++)
        {
            if (x[i] == y[i])
            {
                continue;
            }

            return x[i] > y[i] ? -1 : 1;
        }

        return 0;
    }
}
