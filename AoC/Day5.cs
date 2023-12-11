
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Day5
{
    List<long> Seeds = new List<long>();
    List<long> SeedsRanged = new List<long>();
    Map seedSoil = new Map();
    Map soilFertilizer = new Map();
    Map fertilizerWater = new Map();
    Map waterLight = new Map();
    Map lightTemperature = new Map();
    Map temperatureHumidity = new Map();
    Map HumidityLocation = new Map();

    public string Main()
    {


        string input = File.ReadAllText(@"C:\Privat\AoC\AoC\inputs\5.txt");
        InputReader(input);

        for (int i = 0; i < Seeds.Count; i += 2)
        {
            SeedsRanged.AddRange(CreateRange(Seeds[i], Seeds[i+1]));
        }
        List<long> locations = new List<long>(); 
        var max = SeedsRanged.Count;
        var counter = 0;
        var t = new System.Threading.Timer((s)=>Console.WriteLine($"{counter}/{max}"),null,0,60000);
        foreach (var seed in SeedsRanged)
        {
            counter += 1;
            var a = seedSoil.GetDestinationFromSource(seed);
            var b = soilFertilizer.GetDestinationFromSource(a);
            var c = fertilizerWater.GetDestinationFromSource(b);
            var d = waterLight.GetDestinationFromSource(c);
            var e = lightTemperature.GetDestinationFromSource(d);
            var f = temperatureHumidity.GetDestinationFromSource(e);
            var g = HumidityLocation.GetDestinationFromSource(f);
            locations.Add(g);
        }
        t.Dispose();
        return locations.Min().ToString();
    }

    public IEnumerable<long> CreateRange(long start, long count)
{
    var limit = start + count;

    while (start < limit)
    {
        yield return start;
        start++;
    }
}

    public void InputReader(string input)
    {
        Seeds =  Regex.Matches(input, @"seeds:.(.+)")[0].Groups[1].Value.Split(' ').Select(x=>long.Parse(x)).ToList();

        string[] lines = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        var map = new Map();
        for (int i = 1; i < lines.Length; i++)
        {
            switch (lines[i])
            {
                case "seed-to-soil map:":
                    map = seedSoil;
                    continue;
                case "soil-to-fertilizer map:":
                    map = soilFertilizer;
                    continue;
                case "fertilizer-to-water map:":
                    map = fertilizerWater;
                    continue;
                case "water-to-light map:":
                    map = waterLight;
                    continue;
                case "light-to-temperature map:":
                    map = lightTemperature;
                    continue;
                case "temperature-to-humidity map:":
                    map = temperatureHumidity;
                    continue;
                case "humidity-to-location map:":
                    map = HumidityLocation;
                    continue;
                default:
                    break;
            }

            if (Regex.Matches(lines[i], @"\d+").Count > 0)
            {
                var split = lines[i].Split(' ');
                map.Combinations.Add(new Combination(){
                    Destination = long.Parse(split[0]),
                    Source = long.Parse(split[1]),
                    Range = long.Parse(split[2])
                });
            }
        }
    }

    public class Map
    {
        public List<Combination> Combinations { get; set; } = new List<Combination>();
        public List<(long destination, long source)> DestinationSource {get; set;} = new List<(long, long)>();

        // public void Generate()
        // {
        //     foreach (var item in Combinations)
        //     {
        //         for (int i = 0; i < item.Range; i++)
        //         {
        //             DestinationSource.Add(new (item.Destination + i, item.Source + i));
        //         }
        //     }

        // }

        public long GetDestinationFromSource(long source)
        {
            foreach (var comb in Combinations)
            {
                if (source >= comb.Source && source < comb.SourceEnd)
                {
                    return comb.Destination + (source - comb.Source);
                }
            }
            //var result = DestinationSource.FirstOrDefault(x => x.source == source).destination;
            return source;
        }
    }

    public class Combination
    {
        public long Source { get; set; }
        public long SourceEnd {get {return Source + Range;} }
        public long Destination { get; set; }
        public long Range { get; set; }
    }
}
