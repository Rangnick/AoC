using System.Text.RegularExpressions;
using System.Xml.XPath;

class Program
{
    static void Main(string[] args)
    {
        var key = Console.ReadLine();

        switch (key)
        {
            case "1":
                Console.WriteLine(Day1.Main());
                break;
            case "2":
                Console.WriteLine(Day2.Main());
                break;
            case "3":
                Console.WriteLine(Day3.Main());
                break;
            case "4":
                Console.WriteLine(Day4.Main());
                break;
            case "5":
                Console.WriteLine(new Day5().Main());
                break;
            default:
                Console.WriteLine("Wrong input");
                break;
        }
        Console.Read();
    }
}