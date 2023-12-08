public static class Day2
{
    static List<Game>? Games = new();

    public static string Main()
    {
        string input = File.ReadAllText(@"inputs\2.txt");

        Converter(input);
        // var results = Games.Where(x => x.Sets.Any(y => y.Red > 12 || y.Green > 13 || y.Blue > 14)).Select(x => x.Id);
        // Games.RemoveAll(x => results.Contains(x.Id));
        // var sum = Games.Sum(x => x.Id);
        return Games.Sum(x => x.Sets.Max(y => y.Red) * x.Sets.Max(y => y.Blue) * x.Sets.Max(y => y.Green)).ToString();
    }

    private static void Converter(string input)
    {
        input = input.Replace("Game ", "");

        var tmp = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
        foreach (var line in tmp)
        {
            var game = new Game();
            game.Id = Int32.Parse(line.Split(':').First());
            var newLine = line.Replace($"{game.Id}: ", "");

            var sets = newLine.Split(';');
            foreach (string set in sets)
            {
                Set tmpSet = new Set();
                var cubes = set.Split(',');
                foreach (string cube in cubes)
                {
                    if(cube.Contains("green"))
                    {
                        var c = cube.Replace(" green", "").Trim();
                        tmpSet.Green = Int32.Parse(c);
                    }
                    else if(cube.Contains("blue"))
                    {
                        var c = cube.Replace(" blue", "").Trim();
                        tmpSet.Blue = Int32.Parse(c);
                    }
                    else if(cube.Contains("red"))
                    {
                        var c = cube.Replace(" red", "").Trim();
                        tmpSet.Red = Int32.Parse(c);
                    }
                }
                game.Sets.Add(tmpSet);
            } 
            Games.Add(game); 
        }
    }
}

class Set
{
    public int Red;
    public int Blue;
    public int Green;
}

class Game
{
    public int Id;
    public List<Set> Sets = new List<Set>();
}