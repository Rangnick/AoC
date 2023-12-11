
using System.Text.RegularExpressions;

public static class Day4
{
    public static string Main()
    {
        var result = 0;
        var result2 = 0;
        string input = File.ReadAllText(@"C:\Privat\AoC\AoC\inputs\4.txt");
        List<int> numbers = new List<int>();

        var reg1 = @":(.+)\|{1}";
        var reg2 = @"\|{1}(.+)\n";
        MatchCollection matches =  Regex.Matches(input, reg1);
        MatchCollection matches2 =  Regex.Matches(input, reg2);

        for (int i = 0; i < matches.Count; i++)
        {
            var winningNumbers = Regex.Matches(matches[i].ToString(), @"\d+").Select(x => Int32.Parse(x.Value)).ToList();
            var myNumbers = Regex.Matches(matches2[i].ToString(), @"\d+").Select(x => Int32.Parse(x.Value)).ToList();
            var hit = winningNumbers.Intersect(myNumbers).ToList();
            var hits = hit.Count;
            result += GetWinningPointsFromMatches(hits);
        }

        return DoGamePart2(input).ToString();
    }

    static int DoGamePart2(string fullInput)
    {
        string[] cards = fullInput
            .Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        cards = cards.Take(cards.Count() - 1).ToArray();

        int[] numberOfCards = Enumerable.Repeat(1, cards.Length).ToArray();
        for (int index = 0; index < numberOfCards.Length; index++)
            AddCopies(numberOfCards, index, CountOfWinningNumbers(cards[index]));

        return numberOfCards.Sum();

    }

    static void AddCopies(int[] numberOfCards, int index, int numberOfCardsToCopy)
    {
        int indexOfFirstCardToCopy = index + 1;
        for (int i = 0; i < numberOfCardsToCopy; i++)
        {
            if (indexOfFirstCardToCopy + i == numberOfCards.Length)
                break;
            numberOfCards[indexOfFirstCardToCopy + i] += numberOfCards[index];
        }
    }

    static int CountOfWinningNumbers(string card)
    {
        var (winningNumbers, drawnNumbers) = ParseCard(card);
        return drawnNumbers
            .Intersect(winningNumbers)
            .Count();
    }

    static (int[] winningNumbers, int[] drawnNumbers) ParseCard(string card)
    {
        string withoutLabel = card.Split(":")[1];
        int[] winningNumbers = withoutLabel
            .Split("|")[0]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(el => int.Parse(el.Trim()))
            .ToArray();
        int[] drawnNumbers = withoutLabel
            .Split("|")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(el => int.Parse(el.Trim()))
            .ToArray();
        return (winningNumbers, drawnNumbers);
    }

    public static int GetWinningPointsFromMatches(int hits)
    {
        if (hits == 0)
        {
            return 0;
        }

        return (int)Math.Pow(2, hits -1 );
    }
}
