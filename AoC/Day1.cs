using System.Text.RegularExpressions;

public static class Day1
{
    public static string Main()
    {
        string input = File.ReadAllText(@"inputs\1.txt");; 
        return Part1(Part2(input));
    }

    private static string Part1(string s)
    {
        Console.WriteLine("Start Tag 1\r");

        int result = 0;
        List<string> tmp = s.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
        var searchPattern = @"\d+";

        foreach (var line in tmp)
        {
            string matches = string.Join("", Regex.Matches(line, searchPattern));
            if (matches.Length > 0)
            {
                var number = matches.Substring(0, 1) + matches.Substring(matches.Length -1);
                result += Int32.Parse(number);
            }
        }
        return result.ToString();
    }

    private static string Part2(string s)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>(){
            {"twone","21"},
            {"threeight","38"},
            {"eighthree","83"},
            {"eightwo","82"},
            {"oneight","18"},
            {"fiveight","58"},
            {"nineight","98"},  
            {"one", "1"},   //oe
            {"two","2"},    //wo
            {"three","3"},  //te
            {"four","4"},   //fr
            {"five","5"},   //fe
            {"six","6"},    //sx
            {"seven","7"},  //sn
            {"eight","8"},  //et
            {"nine","9"},   //ne
          
        };

        return dic.Aggregate(s, (current, value) => 
            current.Replace(value.Key, value.Value));
    }
}